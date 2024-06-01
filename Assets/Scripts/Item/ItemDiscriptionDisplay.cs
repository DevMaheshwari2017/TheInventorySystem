using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemDiscriptionDisplay : MonoBehaviour
{

    private SO itemSO;

    [SerializeField]
    private Button closeDiscWindowButton;
    [SerializeField]
    private Button addButton;
    [SerializeField]
    private Button subButton;
    [SerializeField]
    private TextMeshProUGUI quantityText;
    [SerializeField]
    private RectTransform discWindow;
    [SerializeField]
    private Image img;
    [SerializeField]
    private TextMeshProUGUI weight;
    [SerializeField]
    private TextMeshProUGUI itemDiscription;
    [SerializeField]
    private TextMeshProUGUI rarity;
    [SerializeField]
    private TextMeshProUGUI itemtypes;
    [SerializeField]
    private GameObject buyCost;
    [SerializeField]
    private GameObject sellCost;
    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private Button sellButton;
    [SerializeField]
    private Button buyButton;

    private HoverInput hoverInput;
    private int quantity;

    //Getter
    public SO GetItemSO() => itemSO;

    //setter
    public void SetItemSO(SO newItemSO) 
    {
        itemSO = newItemSO;
    }

    public void SetHoverInput(HoverInput hoverInput) 
    {
        this.hoverInput = hoverInput;
    }
    private void OnEnable()
    {
        EventService.Instance.OnShowDiscWindow.AddListner(ShowDisplay);        
    }
    private void OnDisable()
    {
        EventService.Instance.OnShowDiscWindow.RemoveListner(ShowDisplay);
    }
    private void Update()
    {
        quantityText.text = quantity.ToString();
    }
    private void Start()
    {
        sellButton.onClick.AddListener(SellButton);
        buyButton.onClick.AddListener(BuyButton);
        closeDiscWindowButton.onClick.AddListener(HideDiscDisplay);
        addButton.onClick.AddListener(IncreaseQuantity);
        subButton.onClick.AddListener(DecereaseQuantity);
        HideDiscDisplay();
    }

    private void ShowDisplay( Vector2 mousePos) 
    {
        if (itemSO == null)
        {
            Debug.LogError("ItemSO is null.");
            return;
        }
        img.sprite = itemSO.img;
        weight.text = " Weight : " + itemSO.weight.ToString();
        itemDiscription.text = itemSO.itemDiscription;
        rarity.text = itemSO.rarity.ToString();
        itemtypes.text = " Type : " + itemSO.itemtypes.ToString();
        itemName.text = itemSO.itemName;


        buyCost.GetComponent<TextMeshProUGUI>().text = " Buy Cost : " + itemSO.buyCost.ToString();
        sellCost.GetComponent<TextMeshProUGUI>().text = " Sell Cost : " + itemSO.sellCost.ToString();

        
        if (hoverInput.GetIsItemInInevntory())
        {
            ShowSellText();
            sellButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            ShowBuyText();
            sellButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
        }

        discWindow.gameObject.SetActive(true);
        ShowDiscWindowAtCentre(mousePos);
    }

    private void ShowDiscWindowAtCentre(Vector2 mousePos) 
    {
        RectTransform canvasRectTransform = GetComponent<RectTransform>();
        if (canvasRectTransform == null)
        {
            Debug.LogError("Canvas RectTransform is not found.");
            return;
        }

        bool isConverted = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, mousePos, null, out Vector2 canvasPos);

        if (!isConverted)
        {
            Debug.LogError("Failed to convert screen point to local point.");
            return;
        }

        // Adjust the position of the description window to center it at the cursor
        Vector2 adjustedPosition = new Vector2(
            Mathf.Clamp(canvasPos.x, discWindow.sizeDelta.x * canvasRectTransform.localScale.x / 2, canvasRectTransform.rect.width - discWindow.sizeDelta.x * canvasRectTransform.localScale.x / 2),
            Mathf.Clamp(canvasPos.y, discWindow.sizeDelta.y * canvasRectTransform.localScale.y / 2, canvasRectTransform.rect.height - discWindow.sizeDelta.y * canvasRectTransform.localScale.y / 2)
        );

        discWindow.anchoredPosition = adjustedPosition;
    }

    private void BuyButton()
    {
        EventService.Instance.OnBuyingItemDecreaseCoin?.InvokeEvent(quantity * itemSO.buyCost);
        EventService.Instance.OnBuyingItemIncreaesWeight?.InvokeEvent(quantity * itemSO.weight);
    }
    private void SellButton()
    {
        EventService.Instance.OnBuyingItemDecreaseCoin?.InvokeEvent(quantity * itemSO.sellCost);
        EventService.Instance.OnSellingItemDecreaseWeight?.InvokeEvent(quantity * itemSO.weight);
    }

    private void IncreaseQuantity()
    {
        if (quantity < hoverInput.GetItemDisplay().GetTotalItemQuantity())
            quantity++;
    }

    private void DecereaseQuantity()
    {
        if (quantity > 0)
            quantity--;
    }

    public void ShowSellText()
    {
          buyCost.SetActive(false);
          sellCost.SetActive(true);
    }

    private void ShowBuyText() 
    {
        buyCost.SetActive(true);
        sellCost.SetActive(false);
    }

    public void HideDiscDisplay() 
    {
        discWindow.gameObject.SetActive(false);
    }

}
