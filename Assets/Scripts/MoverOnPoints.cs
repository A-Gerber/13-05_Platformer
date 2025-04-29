using UnityEngine;

public class MoverOnPoints : MonoBehaviour
{
    private Transform[] _points;
    private int _currentNumber;
    private int _startNumber = 0;
    private float _speedWalk = 0f;
    private float _closeDistance = 0.2f;

    private void Awake()
    {
        _currentNumber = _startNumber;
    }

    public void Init(float speedWalk, Transform route)
    {
        _speedWalk = speedWalk;
        _points = new Transform[route.childCount];

        for (int i = 0; i < route.childCount; i++)
        {
            _points[i] = route.GetChild(i);
        }
    }

    public void Move ()
    {
        Vector2 offset = _points[_currentNumber].transform.position - transform.position;

        if (offset.sqrMagnitude < _closeDistance * _closeDistance)
            _currentNumber = ++_currentNumber % _points.Length;

        transform.position = Vector2.MoveTowards(transform.position, _points[_currentNumber].position, _speedWalk * Time.deltaTime);

        if (offset.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (offset.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void ResetMover()
    {
        _currentNumber = _startNumber;
    }
}