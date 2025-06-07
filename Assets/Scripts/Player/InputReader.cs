using System;
using UnityEngine;
using UnityEngine.UI;

public class InputReader : MonoBehaviour
{
    private const KeyCode JumpButton = KeyCode.Space;
    private const int AttackButton = 0;
    private const string Horizontal = "Horizontal";

    [SerializeField] private Button _vampirismButton;

    private bool _isJump;

    public event Action Attacked;
    public event Action UsedVampirism;

    public float Direction { get; private set; } = 0;

    private void OnEnable()
    {
        _vampirismButton.onClick.AddListener(UseVampirism);
    }

    private void OnDisable()
    {
        _vampirismButton.onClick.RemoveListener(UseVampirism);
    }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpButton))
        {
            _isJump = true;
        }

        if (Input.GetMouseButtonDown(AttackButton))
        {
            Attacked?.Invoke();
        }
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }

    private void UseVampirism() => UsedVampirism?.Invoke();
}