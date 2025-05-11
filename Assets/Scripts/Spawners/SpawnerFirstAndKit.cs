using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerFirstAndKit : MonoBehaviour
{
    [SerializeField] private FirstAndKit _prefab;
    [SerializeField] private int _maxCountCoin = 3;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;
    [SerializeField] private List<Transform> _points;

    private ObjectPool<FirstAndKit> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<FirstAndKit>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (firstAndKit) => OnGet(firstAndKit),
            actionOnRelease: (firstAndKit) => OnRelease(firstAndKit),
            actionOnDestroy: (firstAndKit) => Destroy(firstAndKit),
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

    private void OnRelease(FirstAndKit firstAndKit)
    {
        firstAndKit.gameObject.SetActive(false);
        firstAndKit.PickedUp -= ReleaseCoin;
    }

    private void OnGet(FirstAndKit firstAndKit)
    {
        int randomNumber = UnityEngine.Random.Range(0, _points.Count);
        firstAndKit.transform.position = _points[randomNumber].position;
        _points.RemoveAt(randomNumber);

        firstAndKit.gameObject.SetActive(true);
        firstAndKit.PickedUp += ReleaseCoin;
    }

    private void GetCoin()
    {
        _pool.Get();
    }

    private void ReleaseCoin(FirstAndKit firstAndKit)
    {
        _pool.Release(firstAndKit);
    }
}