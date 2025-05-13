using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationsHandler : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMovement(float value) => _animator.SetFloat(PlayerAnimatorData.Params.Speed, value);

    public void SetAttackStatus(bool isAttack) => _animator.SetBool(EnemyAnimatorData.Params.IsAttack, isAttack);

    public void TriggerAttack() => _animator.SetTrigger(PlayerAnimatorData.Params.Attack);
}
