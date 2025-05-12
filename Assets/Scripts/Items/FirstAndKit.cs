using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FirstAndKit : MonoBehaviour, ICollectable
{
    public float HealingHitPoints { get; private set; } = 30f;

    public event Action<FirstAndKit> PickedUp;

    public void Collect(ItemsPicker itemsPicker)
    {
        itemsPicker.CollectFirstAidKit(this);
        PickedUp?.Invoke(this);
    }
}