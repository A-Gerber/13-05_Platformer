using UnityEngine;

public class HealthView : MonoBehaviour
{
    public void Show(float value)
    {
        Debug.Log(gameObject.name + " - " + value);
    }
}
