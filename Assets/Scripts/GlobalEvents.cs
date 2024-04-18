using UnityEngine.Events;

public class GlobalEvents
{
    public static readonly UnityEvent OnDieEvent = new UnityEvent();
    public static readonly UnityEvent OnHitEvent = new UnityEvent();

    static public void CallOnDieEvent() => OnDieEvent?.Invoke();
    static public void CallOnHitEvent() => OnHitEvent?.Invoke();
}
