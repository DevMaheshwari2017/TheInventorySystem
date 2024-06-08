using UnityEngine;
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
        itemDisplay = GetComponent<ItemDisplay>();

    }
    private void Update()
    {
        if (!isDraggingItem)
        {
            checkForItemSlotEmpty = itemDisplay.IsItemSOEmpty();
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
       
        if (checkForItemSlotEmpty == true 
            || GameService.Instance.GetUIManager().NotEnoughCoin(itemDisplay.GetItemSO().buyCost * itemDisplay.GetTotalItemQuantity())
            || GameService.Instance.GetUIManager().WeightOverload())
            return;

        ItemDisplay itemCloneDisplay;
        int totalQuantity = itemDisplay.GetTotalItemQuantity();

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

        itemCloneDisplay = itemClone.GetComponent<ItemDisplay>();
        if (itemCloneDisplay != null)
        {
            itemCloneDisplay.SetTotalQuantity(totalQuantity);
        }
        else
        {
            Debug.LogWarning("Item clone display is null");
        }
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
            Destroy(itemClone);
            isDraggingItem = false;
        }
        else
        {
            itemDisplay.ClearItemSO();
            rectTransform.anchoredPosition = new Vector2(0, -1);
            itemClone.GetComponent<DragableItem>().enabled = false;
            isDraggingItem = false;
        }
    }
}
