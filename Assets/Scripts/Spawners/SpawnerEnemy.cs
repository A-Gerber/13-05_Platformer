using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private PointSpawnEnemy[] _pointsSpawn;
    [SerializeField] private int _maxCountEnemy = 8;
    [SerializeField] private int _startCountEnemy = 3;
    [SerializeField] private float _repeatRate = 10.0f;
    [SerializeField] private int _poolCapacity = 6;
    [SerializeField] private int _poolMaxSize = 6;

    private ObjectPool<Enemy> _pool;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private int _queueNumber = 0;
    private int _countEnemy = 0;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => CreateEnemy(),
            actionOnGet: (enemy) => OnGet(enemy),
            actionOnRelease: (enemy) => OnRelease(enemy),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        _wait = new WaitForSeconds(_repeatRate);
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(GetEnemyOverTime());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void Start()
    {
        for (int i = 0; i < _startCountEnemy; i++)
        {
            GetEnemy();
        }
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = _pointsSpawn[_queueNumber % _pointsSpawn.Length].CreateEnemy();
        _queueNumber++;

        return enemy;
    }

    private void OnRelease(Enemy enemy)
    {
        enemy.MoverOnPoints.ResetMover();
        enemy.Health.ResetCount();
        enemy.gameObject.SetActive(false);

        enemy.Died -= ReleaseEnemy;
    }

    private void OnGet(Enemy enemy)
    {
        enemy.transform.position = enemy.Birthplace;
        enemy.gameObject.SetActive(true);
        enemy.Health.ResetIndicatorHealth();

        enemy.Died += ReleaseEnemy;
    }

    private void GetEnemy()
    {
        _pool.Get();
        _countEnemy++;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private IEnumerator GetEnemyOverTime()
    {
        while (_countEnemy != _maxCountEnemy)
        {
            yield return _wait;
            GetEnemy();
        }
    }
}