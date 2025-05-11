using System;
using UnityEngine;

public class ItemsPicker : MonoBehaviour
{
    public event Action PickUpedCoin;
    public event Action<float> PickUpedFirstAndKit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            PickUpedCoin?.Invoke();
            coin.PickUp();
        }

        if (collision.TryGetComponent(out FirstAndKit firstAndKit))
        {
            PickUpedFirstAndKit?.Invoke(firstAndKit.HealingHitPoints);
            firstAndKit.PickUp();
        }
    }
}