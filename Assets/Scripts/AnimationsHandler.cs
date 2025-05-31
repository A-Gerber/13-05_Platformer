using UnityEngine;

public class AnimationsHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetMovement(float value) => _animator.SetFloat(PlayerAnimatorData.Params.Speed, value);

    public void SetAttackStatus(bool isAttack) => _animator.SetBool(EnemyAnimatorData.Params.IsAttack, isAttack);

    public void TriggerAttack() => _animator.SetTrigger(PlayerAnimatorData.Params.Attack);
}
