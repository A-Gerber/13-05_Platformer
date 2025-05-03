using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _countCoins = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.PickUp();
            _countCoins++;
        }
    }
}