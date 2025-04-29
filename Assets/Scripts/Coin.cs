using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    private CircleCollider2D  _circleCollider;
    private float _radiusCollider = 0.35f;

    public event Action<Coin> PickedUp;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();

        _circleCollider.isTrigger = true;
        _circleCollider.radius = _radiusCollider;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            PickedUp?.Invoke(this);
        }
    }
}