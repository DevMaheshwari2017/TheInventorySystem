using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private InventorySlot[] inventorySlots;
    [SerializeField]
    private Button getItemButton;
    [SerializeField]
    private int coin;
    [SerializeField]
    private int totalWeight;
    [SerializeField]
    private TextMeshProUGUI weightText;
    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private ItemDiscriptionDisplay itemDiscriptionDisplay;
    [SerializeField]
    private Animator weightOverloadAnimator;
    [SerializeField]
    private GameObject weightOverloadGameobejct;  
    [SerializeField]
    private GameObject notEnoughCoinGameobejct;

    private float weight = 0;

    private void Awake()
    {
        coinText.text = coin.ToString();
        weightText.text = weight.ToString() + " / " + totalWeight.ToString();
    }
    private void OnEnable()
    {
        EventService.Instance.OnBuyingItemDecreaseCoin.AddListner(DecreaseCoinAmount);
        EventService.Instance.OnBuyingItemIncreaesWeight.AddListner(IncreaseWeight);
        EventService.Instance.OnSellingItemIncreaseCoin.AddListner(IncreaseCoinAmount);
        EventService.Instance.OnSellingItemDecreaseWeight.AddListner(DecreaseWeight);
    }

    private void OnDisable()
    {
        EventService.Instance.OnBuyingItemIncreaesWeight.RemoveListner(IncreaseWeight);
        EventService.Instance.OnBuyingItemDecreaseCoin.RemoveListner(DecreaseCoinAmount);
        EventService.Instance.OnSellingItemIncreaseCoin.RemoveListner(IncreaseCoinAmount);
        EventService.Instance.OnSellingItemDecreaseWeight.RemoveListner(DecreaseWeight);
    }
    private void Start()
    {
        getItemButton.onClick.AddListener(GetItems);
    }

    private void GetItems()
    {
        EventService.Instance.OnGetItemEvent.InvokeEvent();
    }

    private void IncreaseCoinAmount(int increaseCoinAmount)
    {
        coin += increaseCoinAmount;
        coinText.text = coin.ToString();
    }
    private void DecreaseCoinAmount(int decreaseCoinAmount)
    {
        coin -= decreaseCoinAmount;
        coinText.text = coin.ToString();
    }
    private void IncreaseWeight(float _weight)
    {
        weight += _weight;
        weightText.text = weight.ToString() + " / " + totalWeight.ToString();
        if (weight >= totalWeight)
        {
            weightText.color = Color.red;
        }
    }
    private void DecreaseWeight(float _weight)
    {
        weight -= _weight;
        weightText.text = weight.ToString() + " / " + totalWeight.ToString();
        if (weight < totalWeight)
        {
           weightText.color = Color.white;          
        }
    }

    public void ItemPurchased(Transform item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].GetItemSO() == null && itemDiscriptionDisplay.GetQuantity() > 0)
            {
                Debug.Log("item purchased and item set to inventory" + inventorySlots[i]);
                inventorySlots[i].SetItemSO(itemDiscriptionDisplay.GetItemSO());
                GameObject itemClone = Instantiate(itemDiscriptionDisplay.GetHoverInput().gameObject, inventorySlots[i].transform);
                UpdateQuantity(itemDiscriptionDisplay.GetQuantity(), itemClone.GetComponent<ItemDisplay>());
                break;
            }
            //else if (inventorySlots[i].GetItemSO() == itemDiscriptionDisplay.GetItemSO())
            //{
            //     += itemDiscriptionDisplay.GetQuantity();
            //}
        }
    }

    private void UpdateQuantity(int quantity, ItemDisplay itemDisplay)
    {
        int totalQauntity = 0;
        Debug.Log("updating quantity ");

        if (quantity > 0)// using manual qunatity optin in disc window 
        {
            totalQauntity += quantity;
            Debug.Log("The total item qunatity in inventory using manual is " + totalQauntity);
        }
        itemDisplay.SetTotalQuantity(totalQauntity);
    }

    private IEnumerator ShowWieghtOverload() {
            weightOverloadAnimator.enabled = true;
            weightOverloadGameobejct.SetActive(true);
            weightOverloadAnimator.Play("WeightOverload");
            yield return new WaitForSeconds(weightOverloadAnimator.runtimeAnimatorController.animationClips.Length * 5);
            weightOverloadAnimator.enabled = false;
            yield return new WaitForSeconds(3.0f);
            weightOverloadGameobejct.SetActive(false);
    }

    private IEnumerator ShowNotEnoughCoin() 
    {
        notEnoughCoinGameobejct.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        notEnoughCoinGameobejct.SetActive(false);

    }

    public bool WeightOverload() 
    {
        Debug.Log("Checking for weight overload");
        if (weight >= totalWeight)
        {          
            Debug.Log("Weight overloaded");
           StartCoroutine(ShowWieghtOverload());
            return true;
        }
       return false;
    }

    public bool NotEnoughCoin(int amount) 
    {
        if (coin < amount)
        {
            Debug.Log("Not enough coin");
           StartCoroutine(ShowNotEnoughCoin());
            return true;
        }
        return false;
    }
}
