using System;
using UnityEngine;

[RequireComponent(typeof(HealthView))]
public class Health : MonoBehaviour
{
    private float _maxCount = 100;
    private float _currentCount;
    private HealthView _healthView;

    public bool IsAlive => _currentCount > 0;

    private void Awake()
    {
        _healthView = GetComponent<HealthView>();
        _currentCount = _maxCount;
    }

    public void TakeDamage(float damage)
    {
        _currentCount -= damage;
        _healthView.Show(_currentCount);
    }

    public void Heal(float healing)
    {
        _currentCount = Mathf.Min(_maxCount, _currentCount + healing);
        _healthView.Show(_currentCount);
    }

    public void ResetCount()
    {
        _currentCount = _maxCount;
    }
}