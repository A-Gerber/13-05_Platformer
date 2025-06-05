using UnityEngine;

public class EnemyDetector
{
    private Collider2D[] _hits;
    private int _enemyLayer = 1 << 6;
    private int _count = 10;

    public EnemyDetector()
    {
        _hits = new Collider2D[_count];
    }

    public bool TryGetNearestEnemyInRadius(out Enemy enemy, Vector2 position, float radius)
    {
        bool isGot = false;
        enemy = null;
        int count = Physics2D.OverlapCircleNonAlloc(position, radius, _hits, _enemyLayer);

        if (count > 0)
        {
            float closestDistance = float.MaxValue;

            foreach (var hit in _hits)
            {
                if (hit != null)
                {
                    float sqrDistance = position.SqrDistance(hit.transform.position);

                    if (sqrDistance < closestDistance)
                    {
                        if (hit.TryGetComponent(out enemy))
                        {
                            closestDistance = sqrDistance;
                            isGot = true;
                        }
                    }
                }
            }
        }

        return isGot;
    }
}