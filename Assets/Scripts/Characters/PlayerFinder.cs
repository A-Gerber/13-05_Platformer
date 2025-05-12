using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _distance = 6f;

    private Vector2 _point;

    public bool TryFindTarget(out Player target, float direction)
    {
        _point = _raycastPoint.position;
        bool isTarget = false;
        target = null;

        RaycastHit2D hit = Physics2D.Raycast(_point, GetDirection(direction), _distance);

        if (hit.collider != null && hit.collider.TryGetComponent(out target))
        {
            isTarget = true;
        }

        return isTarget;
    }

    private Vector2 GetDirection(float direction)
    {
        return direction > 0 ? Vector2.right : Vector2.left;
    }
}