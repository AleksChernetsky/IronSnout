using UnityEngine.Events;

public class GlobalEvents
{
    public static readonly UnityEvent OnDieEvent = new UnityEvent();

    static public void CallOnDieEvent() => OnDieEvent?.Invoke();
}
