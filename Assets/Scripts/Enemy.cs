using UnityEngine;

[RequireComponent(typeof(Animator), typeof(MoverOnPoints))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speedWalk = 0.5f;
    [SerializeField] private Transform _route;

    private Animator _animator;

    public MoverOnPoints MoverOnPoints { get; private set; }
    public Vector3 Birthplace { get; private set; }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        MoverOnPoints = GetComponent<MoverOnPoints>();
    }

    private void Start()
    {
        MoverOnPoints.Init(_speedWalk, _route);
    }

    private void Update()
    {
        MoverOnPoints.Move();

        _animator.SetFloat(PlayerAnimatorData.Params.Speed, _speedWalk);
    }

    public void SetBirthplace(Vector3 position)
    {
        Birthplace = position;
    }

    public void TakeRoute(Transform route)
    {
        _route = route;
    }
}