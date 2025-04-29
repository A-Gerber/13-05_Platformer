using UnityEngine;

public class Mover2D : MonoBehaviour
{
    private Vector2 _defaultDirection = Vector2.right;
    private float _speed = 0.0f;
    private float _direction = 1;

    private void Update()
    {
        transform.Translate(_defaultDirection * _direction * _speed * Time.deltaTime);

        if (_direction > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (_direction < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
    }

    public void SetSpeed(float maxSpeed)
    {
        _speed = maxSpeed;
    }
}