using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private ItemDisplay[] itemDisplays;

    private void OnEnable()
    {
        EventService.Instance.OnGetItemEvent.AddListner(OrganizeItems);
    }
    private void OnDisable()
    {
        EventService.Instance.OnGetItemEvent.RemoveListner(OrganizeItems);
    }
    private void Start()
    {
        itemDisplays = GetComponentsInChildren<ItemDisplay>();
    }

    public void OrganizeItems()
    {
        Dictionary<string, ItemDisplay> itemMap = new Dictionary<string, ItemDisplay>();

        foreach (var itemDisplay in itemDisplays)
        {
            SO item = itemDisplay.GetItemSO();
            if (item == null) continue;
            string uniqueKey = item.name;

            if (itemMap.ContainsKey(uniqueKey))
            {
                itemMap[uniqueKey].UpdateTotalItemQuantity();
                itemDisplay.ClearItemSO();
            }
            else
            {
                itemMap.Add(uniqueKey, itemDisplay);
                itemDisplay.SetItemSO(item); // Set the item and update display
            }
        }
    }
}
