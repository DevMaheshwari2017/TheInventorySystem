using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{

    [Header("Quantity Componenets")]
    [SerializeField]
    private TextMeshProUGUI totalItemQuantityText;

    [Header("Item Refrence")]
    [SerializeField]
    private Image img;
    [SerializeField]
    private ScriptableObjectDataBaseContainer soDataBaseContainer;
    [SerializeField]
    private SO itemSO;

    private int totalSO;
    private int rand;
    private int totalItemQuantity = 0;
    private bool isIconVisible;

    //Getter
    public SO GetItemSO() => itemSO;
    public int GetTotalItemQuantity() => totalItemQuantity;
    public bool GetIsIconVisisble() => isIconVisible;
    public bool IsItemSOEmpty()
    {
        if (itemSO == null)
            return true;
        return false;
    }

    //Setter
    public void SetTotalQuantity(int _quantity)
    {
        totalItemQuantity = _quantity;
        Debug.Log("Set the ttal quantity to " + totalItemQuantity);
    }

    public void SetItemSO(SO item)
    {
        itemSO = item;
    }

    private void OnEnable()
    {
        EventService.Instance.OnGetItemEvent.AddListner(AssignItemSOToItemSlots);
        EventService.Instance.OnGetItemEvent.AddListner(ShowIconImage);
        //EventService.Instance.OnPurchasedItem.AddListner(DecereaseTotalQuntity);
    }
    private void OnDisable()
    {
        EventService.Instance.OnGetItemEvent.RemoveListner(AssignItemSOToItemSlots);
        EventService.Instance.OnGetItemEvent.RemoveListner(ShowIconImage);
        //EventService.Instance.OnPurchasedItem.RemoveListner(DecereaseTotalQuntity);
    }

    private void Update()
    {
        totalItemQuantityText.text = totalItemQuantity.ToString();
    }

    public void ClearItemSO()
    {
        itemSO = null;
        totalItemQuantity = 0;

        HideIconImage();
    }
    public void UpdateTotalItemQuantity()
    {
        totalItemQuantity++;
    }

    private void AssignItemSOToItemSlots()
    {
        if (itemSO == null)
        {
            totalSO = soDataBaseContainer.sos.Length;
            rand = Random.Range(0, totalSO);
            itemSO = soDataBaseContainer.sos[rand];
            img.sprite = itemSO.img;
            UpdateTotalItemQuantity();
        }
    }

    private void ShowIconImage()
    {
        //to set alpha to 1
        if (itemSO != null)
        {
            isIconVisible = true;
            var tempcolor = img.color;
            tempcolor.a = 1f;
            img.color = tempcolor;
        }
    }

    private void HideIconImage()
    {
        //to set alpha to 0
        Debug.Log("Alpha is 0 ");
        isIconVisible = false;
        var tempcolor = img.color;
        tempcolor.a = 0f;
        img.color = tempcolor;
    }

    public void DecereaseTotalQuntity(int quantity) 
    {
        totalItemQuantity -= quantity;
        if (totalItemQuantity <= 0) 
        {
            Debug.Log("clearing the item as quantity is 0 now ");
            ClearItemSO();

            InventorySlot inventorySlot = GetComponentInParent<InventorySlot>();

            if (inventorySlot != null) 
            {
                Debug.Log("Cleaing item from the inventory after selling all of it");
                inventorySlot.ClearItemSO();
            }
        }
        Debug.Log("The total quantity that got minus is " + quantity);
        Debug.Log("Total remaning quantity is " + totalItemQuantity);
    }
}
