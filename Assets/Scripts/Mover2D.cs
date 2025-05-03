using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover2D : MonoBehaviour
{
    [SerializeField ] private float _jumpForce = 250f;

    private Vector2 _defaultDirection = Vector2.right;
    private float _speed = 0.0f;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(float maxSpeed)
    {
        _speed = maxSpeed;
    }

    public void Move(float direction)
    {
        transform.Translate(_defaultDirection * Mathf.Abs(direction) * _speed * Time.deltaTime);
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(new Vector2(0, _jumpForce));
    }
}