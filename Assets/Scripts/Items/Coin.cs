using System;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour, ICollectable
{
    public event Action<Coin> PickedUp;

    public void Collect(ItemsPicker itemsPicker)
    {
        itemsPicker.CollectCoin(this);
        PickedUp?.Invoke(this);
    }
}