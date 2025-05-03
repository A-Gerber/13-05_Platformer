using UnityEngine;

[RequireComponent(typeof(Animator), typeof(MoverOnPoints), typeof(Flipper2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speedWalk = 0.5f;

    private Animator _animator;
    private Flipper2D _flipper;

    public MoverOnPoints MoverOnPoints { get; private set; }
    public Vector3 Birthplace { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        MoverOnPoints = GetComponent<MoverOnPoints>();
        _flipper = GetComponent<Flipper2D>();
    }

    private void Start()
    {
        MoverOnPoints.Init(_speedWalk);
    }

    private void Update()
    {
        MoverOnPoints.Move();
        _flipper.SetDirection(MoverOnPoints.Direction);

        _animator.SetFloat(PlayerAnimatorData.Params.Speed, _speedWalk);
    }

    public void SetBirthplace(Vector3 position)
    {
        Birthplace = position;
    }
}