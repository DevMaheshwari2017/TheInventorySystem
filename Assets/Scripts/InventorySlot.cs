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
            itemSO = itemDisplay.GetItemSO();
            dragableItem.SetParentTransform(transform);
            EventService.Instance.OnBuyingItemDecreaseCoin?.InvokeEvent(itemDisplay.GetTotalItemQuantity() * itemSO.buyCost);
            EventService.Instance.OnBuyingItemIncreaesWeight?.InvokeEvent(itemDisplay.GetTotalItemQuantity() * itemSO.weight);
        }
        else 
        {
            Debug.LogError(" PointerDrag is NUll ");
        }
    }

    public void ClearItemSO() 
    {
        ItemDisplay itemDisplay = GetComponentInChildren<ItemDisplay>();
        itemSO = null;
        Destroy(itemDisplay.gameObject);
    }

}
