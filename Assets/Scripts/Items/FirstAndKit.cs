using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FirstAndKit : MonoBehaviour
{
    public float HealingHitPoints { get; private set; } = 30f;

    public event Action<FirstAndKit> PickedUp;

    public void PickUp()
    {
        PickedUp?.Invoke(this);
    }
}