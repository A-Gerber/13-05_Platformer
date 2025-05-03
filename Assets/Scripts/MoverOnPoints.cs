using UnityEngine;

public class MoverOnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private int _currentIndex;
    private int _startIndex = 0;
    private float _speedWalk = 0f;
    private float _closeDistance = 0.2f;

    public float Direction { get; private set; } = 0f;

    private void Awake()
    {
        _currentIndex = _startIndex;
    }

    public void Move ()
    {
        Vector2 offset = _points[_currentIndex].transform.position - transform.position;

        if (offset.sqrMagnitude < _closeDistance * _closeDistance)
            _currentIndex = ++_currentIndex % _points.Length;

        transform.position = Vector2.MoveTowards(transform.position, _points[_currentIndex].position, _speedWalk * Time.deltaTime);
        Direction = offset.x;
    }

    public void Init(float speedWalk)
    {
        _speedWalk = speedWalk;
    }

    public void SetRoute(Transform[] points)
    {
        _points = points;
    }

    public void ResetMover()
    {
        _currentIndex = _startIndex;
    }
}