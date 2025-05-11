using UnityEngine;

public class AttackerPlayer : MonoBehaviour
{
    private float _damage = 35;
    private bool _isAttack = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttack && collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.TakeDamage(_damage);
        }
    }

    public void SetAttack(bool isAttack)
    {
        _isAttack = isAttack;
    }
}