using System;


public class EventController
{
    private event Action baseEvent;
    public void AddListner(Action listener) => baseEvent += listener;
    public void RemoveListner(Action listener) => baseEvent -= listener;
    public void InvokeEvent() => baseEvent?.Invoke();
}

public class EventController<T> 
{
    private event Action<T> baseEvent;
    public void AddListner(Action<T> listener) => baseEvent += listener;
    public void RemoveListner(Action<T> listener) => baseEvent -= listener;
    public void InvokeEvent(T type) => baseEvent?.Invoke(type);
}
