using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    DragableItem dragableItem;
    [SerializeField]
    private RectTransform droppedRect;
    [SerializeField]
    private GameObject dropped;
    [SerializeField]
    private SO itemSO;

    private bool droppedSuccessfully = false;

    //getter
    public InventorySlot GetInventorySlot() => this;
    public bool GetItemDroppedSuccessfully() => droppedSuccessfully;
    public SO GetItemSO() => itemSO;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop called");
        dropped = eventData.pointerDrag;
        dragableItem = dropped.GetComponent<DragableItem>();
        if (dropped != null && dragableItem != null)
        {
            dragableItem.SetParentTransform(transform);
            itemSO = dragableItem.GetItemDisplay().GetItemSO();
            Debug.Log("Dropping item into slot: " + dropped.name);
            droppedSuccessfully = true;
        }
        else 
        {
            Debug.LogError(" PointerDrag is NUll ");
        }
    }

 
}
