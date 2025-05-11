using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(MoverOnPoints), typeof(Flipper2D))]
[RequireComponent(typeof(Health), typeof(AttackerEnemy), typeof(FinderOfPlayer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speedWalk = 0.5f;
    [SerializeField] private float _speedRun = 3.0f;

    private Animator _animator;
    private Flipper2D _flipper;
    private AttackerEnemy _attackerEnemy;
    private FinderOfPlayer _finderOfPlayer;

    private Player _target;

    public event Action<Enemy> Died;

    public Health Health { get; private set; }
    public MoverOnPoints MoverOnPoints { get; private set; }
    public Vector3 Birthplace { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        MoverOnPoints = GetComponent<MoverOnPoints>();
        _flipper = GetComponent<Flipper2D>();
        Health = GetComponent<Health>();
        _attackerEnemy = GetComponent<AttackerEnemy>();
        _finderOfPlayer = GetComponent<FinderOfPlayer>();
    }

    private void Start()
    {
        MoverOnPoints.Init(_speedWalk);
    }

    private void Update()
    {
        if (_finderOfPlayer.HaveFoundTarget(out _target, MoverOnPoints.Direction))
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

        _animator.SetBool(EnemyAnimatorData.Params.IsAttack, _attackerEnemy.IsAttack);
        _animator.SetFloat(EnemyAnimatorData.Params.Speed, MoverOnPoints.Speed);
    }

    public void SetBirthplace(Vector3 position)
    {
        Birthplace = position;
    }
}