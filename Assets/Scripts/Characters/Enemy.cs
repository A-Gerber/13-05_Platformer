using System;
using UnityEngine;

[RequireComponent(typeof(AnimationsHandler), typeof(MoverOnPoints), typeof(Flipper2D))]
[RequireComponent(typeof(Health), typeof(AttackerEnemy), typeof(PlayerFinder))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speedWalk = 0.5f;
    [SerializeField] private float _speedRun = 3.0f;

    private AnimationsHandler _animationsHandler;
    private Flipper2D _flipper;
    private AttackerEnemy _attackerEnemy;
    private PlayerFinder _finderOfPlayer;

    private Player _target;

    public event Action<Enemy> Died;

    public Health Health { get; private set; }
    public MoverOnPoints MoverOnPoints { get; private set; }
    public Vector3 Birthplace { get; private set; }

    private void Awake()
    {
        _animationsHandler = GetComponent<AnimationsHandler>();
        MoverOnPoints = GetComponent<MoverOnPoints>();
        _flipper = GetComponent<Flipper2D>();
        Health = GetComponent<Health>();
        _attackerEnemy = GetComponent<AttackerEnemy>();
        _finderOfPlayer = GetComponent<PlayerFinder>();
    }

    private void Start()
    {
        MoverOnPoints.Init(_speedWalk);
    }

    private void Update()
    {
        if (_finderOfPlayer.TryFindTarget(out _target, MoverOnPoints.Direction))
        {
            MoverOnPoints.Init(_speedRun);
            MoverOnPoints.FollowTarget(_target);
        }
        else
        {
            MoverOnPoints.Init(_speedWalk);
            MoverOnPoints.Move();
        }

        _flipper.SetDirection(MoverOnPoints.Direction);

        if (Health.IsAlive == false)
        {
            Died?.Invoke(this);
        }

        _animationsHandler.SetAttackStatus(_attackerEnemy.IsAttack);
        _animationsHandler.SetMovement(MoverOnPoints.Speed);
    }

    public void SetBirthplace(Vector3 position)
    {
        Birthplace = position;
    }
}