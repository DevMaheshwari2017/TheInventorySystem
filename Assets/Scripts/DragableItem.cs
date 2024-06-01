using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private InventorySlot inventorySlot;
    [SerializeField]
    private ItemDisplay itemDisplay;

    private Transform parentAfterDrag;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private bool checkForItemSlotEmpty;
    private GameObject itemClone;
    private static bool isDraggingItem;

    //Getter
    public ItemDisplay GetItemDisplay() => itemDisplay;
    public bool GetIsDraggingItem() => isDraggingItem;
   // Setter
    public void SetParentTransform(Transform transform)
    {
        parentAfterDrag = transform;
    }

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        //rectTransform = GetComponent<RectTransform>();
        itemDisplay = GetComponentInParent<ItemDisplay>();

    }
    private void Update()
    {
        if (!isDraggingItem)
        {
            Debug.Log(" Dragging item is " + isDraggingItem);
            checkForItemSlotEmpty = itemDisplay.IsItemSOEmpty();
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
       
        if (checkForItemSlotEmpty == true)
            return;

        isDraggingItem = true;
        itemClone = Instantiate(gameObject, transform.position, Quaternion.identity);
        rectTransform = itemClone.GetComponent<RectTransform>();
        canvasGroup = itemClone.GetComponent<CanvasGroup>();

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        parentAfterDrag = transform.parent;
        itemClone.transform.SetParent(transform.root);
        itemClone.transform.SetAsLastSibling();
        itemClone.transform.localScale = gameObject.transform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (checkForItemSlotEmpty == true)
            return;
        isDraggingItem = true;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (checkForItemSlotEmpty == true)
            return;

        inventorySlot = eventData.pointerEnter.GetComponent<InventorySlot>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        itemClone.transform.SetParent(parentAfterDrag, false);

        if (inventorySlot == null)
        {
            Debug.LogError("InventorySlot empty");
            //rectTransform.anchoredPosition = pos;
            Destroy(itemClone);
            isDraggingItem = false;
        }
        else
        {
            //itemDisplay.ClearItemSO();
            Debug.Log("Found Inventory Slot");
            rectTransform.anchoredPosition = new Vector2(0, -1);
            itemClone.GetComponent<DragableItem>().enabled = false;
            isDraggingItem = false;
        }
        Debug.Log("Parent after drag is : " + parentAfterDrag);
    }
}
