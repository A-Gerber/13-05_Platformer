using UnityEngine;

public class PointSpawnEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _route;

    public Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_prefab);
        enemy.TakeRoute(_route);
        enemy.SetBirthplace(transform.position);

        return enemy;
    }
}