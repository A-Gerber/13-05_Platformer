using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationsHandler), typeof(Mover2D), typeof(InputReader))]
[RequireComponent(typeof(GroundDetector), typeof(Flipper2D), typeof(Inventory))]
[RequireComponent(typeof(ItemsPicker), typeof(Health), typeof(AttackerPlayer))]
[RequireComponent(typeof(VampirismSkill))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 3.0f;

    private AnimationsHandler _animationsHandler;
    private Mover2D _mover;
    private InputReader _inputReader;
    private GroundDetector _groundDetector;
    private Flipper2D _flipper;
    private Inventory _inventory;
    private ItemsPicker _itemsPicker;
    private AttackerPlayer _attackerPlayer;
    private VampirismSkill _vampirism;

    private float _timeOfAttack = 0.22f;
    private WaitForSeconds _wait;
    
    public Health Health { get; private set; }

    private void Awake()
    {
        _animationsHandler = GetComponent<AnimationsHandler>();
        _mover = GetComponent<Mover2D>();
        _inputReader = GetComponent<InputReader>();
        _groundDetector = GetComponent<GroundDetector>();
        _flipper = GetComponent<Flipper2D>();
        _inventory = GetComponent<Inventory>();
        _itemsPicker = GetComponent<ItemsPicker>();
        Health = GetComponent<Health>();
        _attackerPlayer = GetComponent<AttackerPlayer>();
        _vampirism = GetComponent<VampirismSkill>();

        _wait = new WaitForSeconds(_timeOfAttack);
    }

    private void OnEnable()
    {
        _itemsPicker.PickUpedCoin += AddCoin;
        _itemsPicker.FirstAidKitCollected += Heal;

        _vampirism.HealedPlayer += Heal;

        _inputReader.Attacked += Attack;
        _inputReader.UsedVampirism += UseVampirism;
    }

    private void OnDisable()
    {
        _itemsPicker.PickUpedCoin -= AddCoin;
        _itemsPicker.FirstAidKitCollected -= Heal;

        _vampirism.HealedPlayer -= Heal;

        _inputReader.Attacked -= Attack;
        _inputReader.UsedVampirism -= UseVampirism;
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

        _animationsHandler.SetMovement(Mathf.Abs(_inputReader.Direction * _maxSpeed));
    }

    private void UseVampirism() => _vampirism.AbsorbOfLife();

    private void Attack()
    {
        _attackerPlayer.SetAttack(true);
        _animationsHandler.TriggerAttack();
        StartCoroutine(StopAnimationOverTime());
    }

    private IEnumerator StopAnimationOverTime()
    {
        yield return _wait;
        _attackerPlayer.SetAttack(false);
        _animationsHandler.TriggerAttack();
    }

    private void Heal(float healing)
    {
        Health.TakeHealth(healing);
    }

    private void AddCoin()
    {
        _inventory.AddCoin();
    }
}