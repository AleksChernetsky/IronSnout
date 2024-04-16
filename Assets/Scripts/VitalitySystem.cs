using System;

using UnityEngine;

public class VitalitySystem : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float CurrentHealth { get; private set; }
    public float HealthPercentage { get; private set; }

    public event Action OnDeath;
    public event Action OnTakeDamage;
    public event Action<int> OnDecreaseHealth;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth > 0)
        {
            OnTakeDamage?.Invoke();
            OnDecreaseHealth?.Invoke(damage);
            HealthPercentage = CurrentHealth / MaxHealth;
        }
        else if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
            GlobalEvents.CallOnDieEvent();
            CurrentHealth = MaxHealth;
        }
    }
}

 