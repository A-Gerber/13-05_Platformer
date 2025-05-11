using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _countCoins = 0;

    public void AddCoin()
    {
        _countCoins++;
    }
}