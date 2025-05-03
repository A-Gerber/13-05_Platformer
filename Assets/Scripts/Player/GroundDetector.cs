using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 0.2f;
    [SerializeField] private Transform _overlapPoint;

    public bool IsGround { get; private set; } = false;

    private void FixedUpdate()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(_overlapPoint.position, _radius))
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