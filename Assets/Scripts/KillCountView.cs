using TMPro;

using UnityEngine;

public class KillCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    private int _count;

    private void Awake()
    {
        GlobalEvents.OnDieEvent.AddListener(IncreaseCount);
        _countText.text = _count.ToString();
    }

    private void IncreaseCount()
    {
        _count++;
        _countText.text = _count.ToString();
    }
}