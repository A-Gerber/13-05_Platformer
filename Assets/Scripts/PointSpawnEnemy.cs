using UnityEngine;

public class PointSpawnEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform[] _waypoints;

    public Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_prefab);
        enemy.MoverOnPoints.SetRoute(_waypoints);
        enemy.SetBirthplace(transform.position);

        return enemy;
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _waypoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _waypoints[i] = transform.GetChild(i);
    }
#endif
}