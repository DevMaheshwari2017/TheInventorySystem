
using UnityEngine;
public class EventService
{
    private static EventService instance;
    public static EventService Instance
    { get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;

        }
    }

    public EventController OnGetItemEvent { get; private set; }
    public EventController<Vector2> OnShowDiscWindow { get; private set; }
    public EventController OnHideDiscWindow { get; private set; }

    public EventController<float> OnBuyingItemIncreaesWeight { get; private set; }
    public EventController<int> OnBuyingItemDecreaseCoin { get; private set; }
    public EventController<int> OnSellingItemIncreaseCoin { get; private set; }
    public EventController<float> OnSellingItemDecreaseWeight { get; private set; }

    public EventController<int> OnPurchasedItem { get; private set; }
    public EventController<int> OnSoldItem { get; private set; }


    EventService() 
    {
        OnGetItemEvent = new EventController();
        OnHideDiscWindow = new EventController();
        OnShowDiscWindow = new EventController<Vector2>();
        OnBuyingItemIncreaesWeight = new EventController<float>();
        OnBuyingItemDecreaseCoin = new EventController<int>();
        OnSellingItemIncreaseCoin = new EventController<int>();
        OnSellingItemDecreaseWeight = new EventController<float>();
        OnPurchasedItem = new EventController<int>();
        OnSoldItem = new EventController<int>();
    }
}
