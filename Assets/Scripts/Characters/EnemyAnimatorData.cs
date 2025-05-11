using UnityEngine;

public class EnemyAnimatorData
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
    }
}
