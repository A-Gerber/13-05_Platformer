using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const KeyCode JumpButton = KeyCode.Space;
    private readonly string Horizontal = "Horizontal";

    public event Action Jumped;

    public float Direction { get; private set; } = 0;

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(JumpButton))
        {
            Jumped?.Invoke();
        }
    }
}