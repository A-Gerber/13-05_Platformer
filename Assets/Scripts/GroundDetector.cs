using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 0.2f;
    [SerializeField] private Transform _overlapPoint;

    private Collider2D[] _colliders;

    public bool IsGround { get; private set; } = false;

    private void FixedUpdate()
    {
        _colliders = Physics2D.OverlapCircleAll(_overlapPoint.position, _radius);

        foreach (Collider2D collider in _colliders)
        {
            if (collider.TryGetComponent<TilemapCollider2D>(out _))
            {
                IsGround = true;
                return;
            }

            IsGround = false;
        }
    }
}