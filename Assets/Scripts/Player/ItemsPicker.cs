using System;
using UnityEngine;

public class ItemsPicker : MonoBehaviour
{
    public event Action PickUpedCoin;
    public event Action<float> FirstAidKitCollected;

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.TryGetComponent(out ICollectable collectable))

            collectable.Collect(this);
    }

    public void CollectCoin(Coin coin) => PickUpedCoin?.Invoke();

    public void CollectFirstAidKit(FirstAndKit firstAidKit) => FirstAidKitCollected?.Invoke(firstAidKit.HealingHitPoints);
}