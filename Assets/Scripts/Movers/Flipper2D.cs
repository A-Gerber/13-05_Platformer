using UnityEngine;

public class Flipper2D : MonoBehaviour
{
    private Vector2 _rightRotate = new Vector2(0,0);
    private Vector2 _leftRotate = new Vector2(0,180);
    private float _direction = 1;

    private void Update()
    {
        if (_direction > 0)
        {
            transform.rotation = Quaternion.Euler(_rightRotate);
        }
        else if (_direction < 0)
        {
            transform.rotation = Quaternion.Euler(_leftRotate);
        }
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
    }
}