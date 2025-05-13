using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 0.2f;
    [SerializeField] private Transform _overlapPoint;

    private Collider2D[] _hits;
    private int _groundLayer = 1 << 8;
    private int _count = 1;

    public bool IsGround { get; private set; } = false;

    private void Awake()
    {
        _hits = new Collider2D[_count];
    }

    private void FixedUpdate()
    {
        int count = Physics2D.OverlapCircleNonAlloc(_overlapPoint.position, _radius, _hits, _groundLayer);
        IsGround = count > 0 ? true : false;
    }
}