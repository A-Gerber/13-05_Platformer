using UnityEngine;
using UnityEngine.UI;

public class VampirismView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private SpriteRenderer _vampireAura;
    [SerializeField] private VampirismSkill _vampirismSkill;

    private void Awake()
    {
        _vampireAura.transform.localScale *= (_vampirismSkill.Radius + _vampirismSkill.Radius);
    }

    private void OnEnable()
    {
        _vampirismSkill.UsedSkill += EnableAura;
        _vampirismSkill.FinishedSkill += DisableAura;
        _vampirismSkill.ChangedValueSlider += SetSliderValue;
    }

    private void OnDisable()
    {
        _vampirismSkill.UsedSkill -= EnableAura;
        _vampirismSkill.FinishedSkill -= DisableAura;
        _vampirismSkill.ChangedValueSlider -= SetSliderValue;
    }

    private void SetSliderValue(float value)
    {
        _slider.value = value;
    }

    private void EnableAura()
    {
        _vampireAura.gameObject.SetActive(true);
    }

    private void DisableAura()
    {
        _vampireAura.gameObject.SetActive(false);
    }
}