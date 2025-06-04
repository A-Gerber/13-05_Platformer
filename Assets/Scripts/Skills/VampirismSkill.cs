using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampirismSkill : MonoBehaviour
{
    [SerializeField] private Button _button;

    private EnemyDetector _enemyDetector = new();
    private Enemy _target;
    private WaitForSeconds _wait;

    private float _sumDamage = 0f;
    private float _damage = 0.5f;
    private float _healCoefficient = 0.5f;
    private float _cooldown = 4f;
    private float _delay = 2f;

    private bool _isAbsorb = false;
    private bool _isCooldown = false;

    public event Action<float> HealedPlayer;
    public event Action UsedSkill;
    public event Action<float> FinishedSkill;

    public float ActionTime { get; private set; } = 6f;
    public float Radius { get; private set; } = 4f;
    public  Vector3 Offset{ get; private set; } = new Vector2(0, 1);

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(AbsorbOfLife);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(AbsorbOfLife);
    }

    private void FixedUpdate()
    {
        if (_isAbsorb && _enemyDetector.TryGetNearestEnemyInRadius(out _target, transform.position + Offset, Radius))
        {
            _target.Health.TakeDamage(_damage);
            _sumDamage += _damage;
        }
    }

    private void AbsorbOfLife()
    {
        if (_isCooldown || _isAbsorb)
        {
            return;
        }

        _isAbsorb = true;
        StartCoroutine(Wait(ActionTime));
        UsedSkill?.Invoke();
    }

    private void UseFinalAct ()
    {
        if (_isAbsorb)
        {
            HealedPlayer?.Invoke(_sumDamage * _healCoefficient);
            _sumDamage = 0;
            _isAbsorb = false;
            _isCooldown = true;
            FinishedSkill?.Invoke(_cooldown);

            StartCoroutine(Wait(_cooldown));
        }
        else
        {
            _isCooldown = false;
        }       
    }

    private IEnumerator Wait(float time)
    {
        for (int i = 0; i < time / _delay; i++)
        {
            yield return _wait;
        }

        UseFinalAct();
    }
}
