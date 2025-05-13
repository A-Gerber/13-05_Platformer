using UnityEngine;

public class Health : MonoBehaviour
{
    private float _maxCount = 100;
    private float _currentCount;

    public bool IsAlive => _currentCount > 0;

    private void Awake()
    {
        _currentCount = _maxCount;
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)        
            _currentCount -= damage;       
    }

    public void Heal(float healing)
    {
        if (healing > 0)
            _currentCount = Mathf.Min(_maxCount, _currentCount + healing);
    }

    public void ResetCount()
    {
        _currentCount = _maxCount;
    }
}