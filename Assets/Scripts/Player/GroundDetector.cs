using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 0.2f;
    [SerializeField] private Transform _overlapPoint;

    private int groundLayer = 1 << 8;

    public bool IsGround { get; private set; } = false;

    private void FixedUpdate()
    {
        IsGround = Physics2D.OverlapCircle(_overlapPoint.position, _radius, groundLayer);
    }
}