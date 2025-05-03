using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode JumpButton = KeyCode.Space;
    private const string Horizontal = "Horizontal";

    private bool _isJump;

    public float Direction { get; private set; } = 0;

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpButton))
        {
            _isJump = true;
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