
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

    EventService() 
    {
        OnGetItemEvent = new EventController();
        OnHideDiscWindow = new EventController();
        OnShowDiscWindow = new EventController<Vector2>();
    }
}
