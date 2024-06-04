using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeButtons : MonoBehaviour
{
    [SerializeField]
    private ItemDisplay[] itemDisplay;
    public void ShowConsumables() {

        for (int i = 0; i <itemDisplay.Length; i++)
        {
            if (itemDisplay[i].GetItemSO() != null && itemDisplay[i].GetItemSO().itemtypes != Itemtypes.Consumable) 
            {
                itemDisplay[i].gameObject.SetActive(false);
            }
            else
            {
                itemDisplay[i].gameObject.SetActive(true);
            }
        }
    }

    public void ShowWeapons() 
    {
        for (int i = 0; i < itemDisplay.Length; i++)
        {
            if (itemDisplay[i].GetItemSO() != null && itemDisplay[i].GetItemSO().itemtypes != Itemtypes.Weapon)
            {
                itemDisplay[i].gameObject.SetActive(false);
            }
            else 
            {
                itemDisplay[i].gameObject.SetActive(true);
            }
        }
    }

    public void ShowTreasures() 
    {
        for (int i = 0; i < itemDisplay.Length; i++)
        {
            if (itemDisplay[i].GetItemSO() != null && itemDisplay[i].GetItemSO().itemtypes != Itemtypes.Treasure)
            {
                itemDisplay[i].gameObject.SetActive(false);
            }
            else
            {
                itemDisplay[i].gameObject.SetActive(true);
            }
        }
    }

    public void ShowMaterials() 
    {
        for (int i = 0; i < itemDisplay.Length; i++)
        {
            if (itemDisplay[i].GetItemSO() != null && itemDisplay[i].GetItemSO().itemtypes != Itemtypes.Material)
            {
                itemDisplay[i].gameObject.SetActive(false);
            }
            else
            {
                itemDisplay[i].gameObject.SetActive(true);
            }
        }
    }
}
