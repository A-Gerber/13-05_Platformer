using UnityEngine;

public class FinderOfPlayer : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _distance = 6f;

    private Vector2 _point;

    public bool HaveFoundTarget(out Player target, float direction)
    {
        _point = _raycastPoint.position;
        bool isTarget = false;
        target = null;

        RaycastHit2D tar = Physics2D.Raycast(_point, SetDirection(direction), _distance);

        if (tar.collider != null && tar.collider.TryGetComponent(out Player player))
        {
            target = player;
            isTarget = true;
        }

        return isTarget;
    }

    private Vector2 SetDirection(float direction)
    {
        if (direction > 0)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.left;
        }
    }
}