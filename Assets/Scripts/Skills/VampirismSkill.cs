using System;
using System.Collections;
using UnityEngine;

public class VampirismSkill : MonoBehaviour
{
    private const float ErrorRate = 0.005f;

    [SerializeField] private Transform _vampireAura;

    private EnemyDetector _enemyDetector = new();
    private WaitForSeconds _wait;

    private float _damage = 5f;
    private float _healCoefficient = 0.5f;
    private float _cooldown = 4f;
    private float _delay = 0.2f;

    private bool _isAbsorb = false;
    private bool _isCooldown = false;

    public event Action UsedSkill;
    public event Action FinishedSkill;
    public event Action<float> HealedPlayer;
    public event Action<float> ChangedValueSlider;

    public float ActionTime { get; private set; } = 6f;
    public float Radius { get; private set; } = 4f;
    public Vector3 Offset { get; private set; } = new Vector2(0, 1);

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    public void AbsorbOfLife()
    {
        if (_isCooldown || _isAbsorb)       
            return;
        
        StartCoroutine(AbsorbOfLifeOverTime());
    }

    private IEnumerator AbsorbOfLifeOverTime()
    {
        _isAbsorb = true;
        float time = 0;
        Enemy target;

        UsedSkill?.Invoke();

        while (ActionTime - time > ErrorRate)
        {
            yield return _wait;
            time += _delay;
            ChangedValueSlider?.Invoke(1 - time/ ActionTime);
            
            if (_enemyDetector.TryGetNearestEnemyInRadius(out target, transform.position + Offset, Radius))
            {
                if (target.Health.GetCount - _damage >= 0)               
                    HealedPlayer?.Invoke(_damage * _healCoefficient);                
                else               
                    HealedPlayer?.Invoke(target.Health.GetCount * _healCoefficient);               

                target.Health.TakeDamage(_damage);
            }           
        }

        _isAbsorb = false;
        _isCooldown = true;
        FinishedSkill?.Invoke();

        StartCoroutine(WaitCooldown());
    }

    private IEnumerator WaitCooldown()
    {
        float time = 0;

        while (_cooldown - time > ErrorRate)
        {
            yield return _wait;
            time += _delay;
            ChangedValueSlider?.Invoke(time / _cooldown);
        }

        _isCooldown = false;
    }
}