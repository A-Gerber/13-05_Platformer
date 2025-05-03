using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    private CircleCollider2D  _circleCollider;
    private float _radiusCollider = 0.35f;

    public event Action<Coin> PickedUpCoin;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();

        _circleCollider.isTrigger = true;
        _circleCollider.radius = _radiusCollider;
    }

    public void  SendToPool ()
    {
        PickedUpCoin?.Invoke(this);
    }
}