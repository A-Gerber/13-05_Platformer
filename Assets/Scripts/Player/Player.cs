using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Mover2D), typeof(InputReader))]
[RequireComponent(typeof(GroundDetector), typeof(Flipper2D), typeof(Wallet))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 3.0f;

    private Animator _animator;
    private Mover2D _mover;
    private InputReader _inputReader;
    private GroundDetector _groundDetector;
    private Flipper2D _flipper;
    private Wallet _wallet;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<Mover2D>();
        _inputReader = GetComponent<InputReader>();
        _groundDetector = GetComponent<GroundDetector>();
        _flipper = GetComponent<Flipper2D>();
        _wallet = GetComponent<Wallet>();
    }

    private void Start()
    {
        _mover.SetSpeed(_maxSpeed);
    }

    private void FixedUpdate()
    {
        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _mover.Jump();
        }
    }

    private void Update()
    {
        _mover.Move(_inputReader.Direction);
        _flipper.SetDirection(_inputReader.Direction);

        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(_maxSpeed * _inputReader.Direction));
    }
}