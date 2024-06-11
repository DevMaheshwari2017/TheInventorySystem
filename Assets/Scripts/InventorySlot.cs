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

            if (GameService.Instance.GetUIManager().NotEnoughCoin(itemDisplay.GetItemSO().buyCost * itemDisplay.GetTotalItemQuantity())
            || GameService.Instance.GetUIManager().WeightOverload(itemDisplay.GetItemSO().weight * itemDisplay.GetTotalItemQuantity()))
            {
                Debug.Log("On Drop weight overload"); 
                return;
            }

            itemSO = itemDisplay.GetItemSO();
            dragableItem.SetParentTransform(transform);
            EventService.Instance.OnBuyingItemDecreaseCoin?.InvokeEvent(itemDisplay.GetTotalItemQuantity() * itemSO.buyCost);
            EventService.Instance.OnBuyingItemIncreaesWeight?.InvokeEvent(itemDisplay.GetTotalItemQuantity() * itemSO.weight);

            if (GameService.Instance.GetUIManager().DroppingSImiliarItemInInventory(itemDisplay)) 
            {
                Debug.Log("Destroying item clone ");
                dragableItem.DestryItemCLone();
                itemSO = null;
            }
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
