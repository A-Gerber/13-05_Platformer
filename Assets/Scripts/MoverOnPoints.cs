using UnityEngine;

public class MoverOnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private int _currentNumber;
    private int _startNumber = 0;
    private float _speedWalk = 0f;
    private float _closeDistance = 0.2f;
    private float _direction = 0f;

    private void Awake()
    {
        _currentNumber = _startNumber;
    }

    public void Move ()
    {
        Vector2 offset = _points[_currentNumber].transform.position - transform.position;

        if (offset.sqrMagnitude < _closeDistance * _closeDistance)
            _currentNumber = ++_currentNumber % _points.Length;

        transform.position = Vector2.MoveTowards(transform.position, _points[_currentNumber].position, _speedWalk * Time.deltaTime);
        _direction = offset.x;
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
        _currentNumber = _startNumber;
    }

    public float GetDirection()
    {
        return _direction;
    }
}