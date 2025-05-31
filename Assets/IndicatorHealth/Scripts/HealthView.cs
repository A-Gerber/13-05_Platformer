using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.Changed += Show;
    }

    private void OnDisable()
    {
        Health.Changed -= Show;
    }

    abstract protected void Show(float value);
}
