using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverInput : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private string itemCloneName;
    [SerializeField]
    private ItemDiscriptionDisplay itemDiscriptionDisplay;

    private ItemDisplay itemDisplay;
    private InventorySlot inventorySlot;
    private float timeToWait = 1f;
    private bool isItemInInventory;

    //getter
    public bool GetIsItemInInevntory() => isItemInInventory;
    public ItemDisplay GetItemDisplay() => itemDisplay;
    private void Awake()
    {
        itemDisplay = GetComponentInParent<ItemDisplay>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragableItem>().GetIsDraggingItem())
            return;

        //if (eventData.pointerDrag != null)
        //    Debug.Log(" dragging item " + eventData.pointerDrag + " " + eventData.pointerDrag.GetComponent<DragableItem>().GetIsDraggingItem());
        if (eventData.pointerEnter.name == itemCloneName) 
        {
            Debug.Log("Got the inventoryslot");
            inventorySlot = GetComponentInParent<InventorySlot>();
        }
        itemDiscriptionDisplay.SetHoverInput(this);
        StartCoroutine(TimeToShowDisplay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Stopping the showdisplay coroutine so it doesn't show the display even after exiting the hover 
        StopAllCoroutines();
    }
    private IEnumerator TimeToShowDisplay() 
    {

        yield return new WaitForSeconds(timeToWait);
        if (itemDisplay != null)
        {
            isItemInInventory = false;
            GameService.Instance.GetItemDiscDisplay().SetItemSO(itemDisplay.GetItemSO());
            EventService.Instance.OnShowDiscWindow?.InvokeEvent(Input.mousePosition);
        }
        else
        {
            isItemInInventory = true;
            GameService.Instance.GetItemDiscDisplay().SetItemSO(inventorySlot.GetItemSO());
            EventService.Instance.OnShowDiscWindow?.InvokeEvent(Input.mousePosition);
        }
    }

}
