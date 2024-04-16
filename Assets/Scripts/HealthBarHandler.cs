using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    [SerializeField] private VitalitySystem _player;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _player.OnTakeDamage += ChangeBar;
    }
    private void Start()
    {
        _slider.maxValue = _player.MaxHealth;
        _slider.value = _player.MaxHealth;
    }
    private void ChangeBar()
    {
        _slider.value = _player.CurrentHealth;
    }
}
