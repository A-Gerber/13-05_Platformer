using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerCoin : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private int _maxCountCoin = 5;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;
    [SerializeField] private List<Transform> _points;

    private ObjectPool<Coin> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (coin) => OnGet(coin),
            actionOnRelease: (coin) => OnRelease(coin),
            actionOnDestroy: (coin) => Destroy(coin),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {      
        for (int i = 0; i < _maxCountCoin; i++)
        {
            GetCoin();
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        _points = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
            _points.Add(transform.GetChild(i));
    }
#endif

    private void OnRelease(Coin coin)
    {
        coin.gameObject.SetActive(false);
        coin.PickedUp -= ReleaseCoin;
    }

    private void OnGet(Coin coin)
    {
        int randomNumber = UnityEngine.Random.Range(0, _points.Count);
        coin.transform.position = _points[randomNumber].position;
        _points.RemoveAt(randomNumber);

        coin.gameObject.SetActive(true);
        coin.PickedUp += ReleaseCoin;
    }

    private void GetCoin()
    {
        _pool.Get();
    }

    private void ReleaseCoin(Coin coin)
    {
        _pool.Release(coin);
    }
}