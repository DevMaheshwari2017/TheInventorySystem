using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private ItemDiscriptionDisplay itemDiscriptionDisplay;
    [SerializeField]
    DragableItem dragableItem;
    [SerializeField]
    private RectTransform droppedRect;
    [SerializeField]
    private GameObject dropped;
    [SerializeField]
    private SO itemSO;

    //Getter
    public SO GetItemSO() => itemSO;

    //setter
    public void SetItemSO(SO _itemSO) 
    {
        itemSO = _itemSO;
    }

    public void OnDrop(PointerEventData eventData)
    {
        dropped = eventData.pointerDrag;
        dragableItem = dropped.GetComponent<DragableItem>();
        if (dropped != null && dragableItem != null)
        {
            //dropped item successfully
            ItemDisplay itemDisplay = dragableItem.GetItemDisplay();
            dragableItem.SetParentTransform(transform);
            itemSO = itemDisplay.GetItemSO();
            EventService.Instance.OnBuyingItemDecreaseCoin?.InvokeEvent(itemDisplay.GetTotalItemQuantity() * itemSO.buyCost);
            EventService.Instance.OnBuyingItemIncreaesWeight?.InvokeEvent(itemDisplay.GetTotalItemQuantity() * itemSO.weight);
        }
        else 
        {
            Debug.LogError(" PointerDrag is NUll ");
        }
    }

    private void ItemPurchased() 
    {
        itemSO = itemDiscriptionDisplay.GetItemSO();
    }
 
}
