using System.Collections;
using UnityEngine;

public class AttackerEnemy : MonoBehaviour
{
    private float _damage = 10f;
    private float _delayBeforeAttack = 0.3f;
    private float _delayAfterAttack = 0.6f;
    private WaitForSeconds _waitBeforeAttack;
    private WaitForSeconds _waitAfterAttack;
    private Coroutine _coroutine;

    public bool IsAttack { get; private set; } = false;

    private void Awake()
    {
        _waitBeforeAttack = new WaitForSeconds(_delayBeforeAttack);
        _waitAfterAttack = new WaitForSeconds(_delayAfterAttack);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {          
            IsAttack = true;

            _coroutine = StartCoroutine(DamageWithDelay(player));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            IsAttack = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    private IEnumerator DamageWithDelay(Player player)
    {
        while (IsAttack)
        {
            yield return _waitBeforeAttack;
            player.Health.TakeDamage(_damage);
            yield return _waitAfterAttack;
        }
    }
}