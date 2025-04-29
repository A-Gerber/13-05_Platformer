using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Mover2D), typeof(InputHandler))]
[RequireComponent(typeof(Jumper))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 6.0f;

    private Animator _animator;
    private Mover2D _mover;
    private InputHandler _inputHandler;
    private Jumper _jumper;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<Mover2D>();
        _inputHandler = GetComponent<InputHandler>();
        _jumper = GetComponent<Jumper>();
    }

    private void Start()
    {
        _mover.SetSpeed(_maxSpeed);
    }

    private void Update()
    {
        _mover.SetDirection(_inputHandler.Direction);

        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(_maxSpeed * _inputHandler.Direction));
    }

    private void OnEnable()
    {
        _inputHandler.Jumped += Jump;
    }

    private void OnDisable()
    {
        _inputHandler.Jumped -= Jump;
    }

    private void Jump()
    {
        _jumper.Jump();
    }
}