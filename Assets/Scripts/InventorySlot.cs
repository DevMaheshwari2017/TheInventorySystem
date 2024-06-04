using System.Threading.Tasks;
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

    private int totalQuanity = 0;
    private float timer = 1f;
    //Getter
    public SO GetItemSO() => itemSO;

    //setter
    public void SetItemSO(SO _itemSO) 
    {
        itemSO = _itemSO;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }
    private void OnEnable()
    {
        //EventService.Instance.OnPurchasedItem.AddListner(UpdateQuantity);
    }

    private void OnDisable()
    {
        //EventService.Instance.OnPurchasedItem.RemoveListner(UpdateQuantity);        
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

}
