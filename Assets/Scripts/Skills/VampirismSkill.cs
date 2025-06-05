using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampirismSkill : MonoBehaviour
{
    private const float ErrorRate = 0.005f;

    [SerializeField] private Button _button;
    [SerializeField] private Transform _vampireAura;

    private EnemyDetector _enemyDetector = new();
    private WaitForSeconds _wait;
    private WaitForSeconds _waitCooldown;

    private float _damage = 5f;
    private float _healCoefficient = 0.5f;
    private float _cooldown = 4f;
    private float _delay = 0.5f;

    private bool _isAbsorb = false;
    private bool _isCooldown = false;

    public event Action<float> HealedPlayer;
    public event Action UsedSkill;
    public event Action<float> FinishedSkill;

    public float ActionTime { get; private set; } = 6f;
    public float Radius { get; private set; } = 4f;
    public Vector3 Offset { get; private set; } = new Vector2(0, 1);

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        _waitCooldown = new WaitForSeconds(_cooldown);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(AbsorbOfLife);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(AbsorbOfLife);
    }

    private void AbsorbOfLife()
    {
        if (_isCooldown || _isAbsorb)
        {
            return;
        }

        StartCoroutine(AbsorbOfLifeOverTime());
        UsedSkill?.Invoke();
    }

    private IEnumerator AbsorbOfLifeOverTime()
    {
        _isAbsorb = true;
        float time = 0;
        Enemy target;

        while (ActionTime - time > ErrorRate)
        {
            yield return _wait;

            if (_isAbsorb && _enemyDetector.TryGetNearestEnemyInRadius(out target, transform.position + Offset, Radius))
            {
                target.Health.TakeDamage(_damage);
                HealedPlayer?.Invoke(_damage * _healCoefficient);
            }

            time += _delay;
        }

        _isAbsorb = false;
        _isCooldown = true;
        FinishedSkill?.Invoke(_cooldown);

        StartCoroutine(WaitCooldown());
    }

    private IEnumerator WaitCooldown()
    {
        yield return _waitCooldown;
        _isCooldown = false;
    }
}