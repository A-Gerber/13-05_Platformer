using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerCoin : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private int _maxCountCoin = 5;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private List<Transform> _points;
    private ObjectPool<Coin> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (enemy) => ActWhenGet(enemy),
            actionOnRelease: (enemy) => ActWhenRelease(enemy),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        _points = new List<Transform>();

        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            _points.Add( _spawnPoints.GetChild(i));
        }

        for (int i = 0; i < _maxCountCoin; i++)
        {
            GetCoin();
        }
    }

    private void ActWhenRelease(Coin coin)
    {
        coin.gameObject.SetActive(false);
        coin.PickedUp -= ReleaseCoin;
    }

    private void ActWhenGet(Coin coin)
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