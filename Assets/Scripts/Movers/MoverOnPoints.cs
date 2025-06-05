using UnityEngine;

public class MoverOnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private int _currentIndex;
    private int _startIndex = 0;
    private float _closeDistance = 0.5f;

    public float Direction { get; private set; } = 0f;
    public float Speed { get; private set; } = 0f;

    private void Awake()
    {
        _currentIndex = _startIndex;
    }

    public void Move()
    {
        if (IsMovingToTarget(_points[_currentIndex]) == false)
        {
            _currentIndex = ++_currentIndex % _points.Length;
        }

        transform.position = Vector2.MoveTowards(transform.position, _points[_currentIndex].position, Speed * Time.deltaTime);
    }

    public void FollowTarget(Player target)
    {
        if (IsMovingToTarget(target.transform))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        }
    }

    public void Init(float speedWalk)
    {
        Speed = speedWalk;
    }

    public void SetRoute(Transform[] points)
    {
        _points = points;
    }

    public void ResetMover()
    {
        _currentIndex = _startIndex;
    }

    private bool IsMovingToTarget(Transform target)
    {
        Vector2 offset = target.transform.position - transform.position;
        Direction = offset.x;

        return offset.sqrMagnitude > _closeDistance * _closeDistance;
    }
}