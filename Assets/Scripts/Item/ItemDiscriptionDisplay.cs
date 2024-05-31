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

    private HoverInput hoverInput;
    private int quantity;

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
        //EventService.Instance.OnHideDiscWindow.AddListner(HideDiscDisplay);
    }
    private void OnDisable()
    {
        EventService.Instance.OnShowDiscWindow.RemoveListner(ShowDisplay);
        //EventService.Instance.OnHideDiscWindow.RemoveListner(HideDiscDisplay);
    }
    private void Update()
    {
        quantityText.text = quantity.ToString();
        //CheckForItemParent();
    }
    private void Start()
    {
        closeDiscWindowButton.onClick.AddListener(HideDiscDisplay);
        addButton.onClick.AddListener(IncreaseQuantity);
        subButton.onClick.AddListener(DecereaseQuantity);
        HideDiscDisplay();
    }

    private void ShowDisplay( Vector2 mousePos) 
    {
        Debug.Log("Inside showDisplay");
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
        }
        else
        {
            ShowBuyText();
        }

        discWindow.gameObject.SetActive(true);
        Debug.Log("Showing Disc window with item" + itemSO);
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

    private void IncreaseQuantity()
    {
        //if (quantity < hoverInput)
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
