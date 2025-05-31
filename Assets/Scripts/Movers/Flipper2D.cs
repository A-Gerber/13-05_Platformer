using UnityEngine;

public class Flipper2D : MonoBehaviour
{
    [SerializeField] private Transform _rotatingModel;

    private Quaternion _rightRotate = Quaternion.Euler(new Vector2(0, 0));
    private Quaternion _leftRotate = Quaternion.Euler(new Vector2(0, 180));
    private float _direction = 1;

    private void Update()
    {
        if (_direction > 0)
        {
            _rotatingModel.transform.rotation = _rightRotate;
        }
        else if (_direction < 0)
        {
            _rotatingModel.transform.rotation = _leftRotate;
        }
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
    }
}