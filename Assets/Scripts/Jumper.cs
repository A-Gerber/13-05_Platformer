using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _touchInaccuracy = 0.1f;
    [SerializeField] private Transform _raycastPoint;

    private bool isGrounded = false;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isGrounded == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(_raycastPoint.position, Vector2.down, _touchInaccuracy);

            if (hit.collider != null)
            {
                isGrounded = hit.transform.TryGetComponent<TilemapCollider2D>(out _);
            }
        }        
    }

    public void Jump()
    {
        if (isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            isGrounded = false;
        }
    }
}