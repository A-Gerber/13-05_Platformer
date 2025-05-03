using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    public event Action<Coin> PickedUp;

    public void  PickUp ()
    {
        PickedUp?.Invoke(this);
    }
}