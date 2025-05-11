using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode JumpButton = KeyCode.Space;
    private const int AttackButton = 0;
    private const string Horizontal = "Horizontal";

    private bool _isJump;

    public event Action Attacked;
    public event Action Jumped;

    public float Direction { get; private set; } = 0;

    private void FixedUpdate()
    {
        if(GetIsJump())
        {
            Jumped?.Invoke();
        }
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
}