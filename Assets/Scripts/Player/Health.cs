using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float MinCount = 0f;

    private float _currentCount;

    public event Action<float> Changed;

    public float MaxCount { get; private set; } = 100f;
    public bool IsAlive => _currentCount > MinCount;

    private void Awake()
    {
        _currentCount = MaxCount;
    }

    private void Start()
    {
        Changed?.Invoke(_currentCount);
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            _currentCount = Mathf.Max(MinCount, _currentCount - damage);

            Changed?.Invoke(_currentCount);
        }
    }

    public void Heal(float healing)
    {
        if (healing > 0)
        {
            _currentCount = Mathf.Min(MaxCount, _currentCount + healing);

            Changed?.Invoke(_currentCount);
        }
    }

    public void ResetCount()
    {
        _currentCount = MaxCount;
    }

    public void ResetIndicatorHealth()
    {
        Changed?.Invoke(_currentCount);
    }
}