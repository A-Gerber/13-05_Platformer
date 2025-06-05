using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampirismView : MonoBehaviour
{
    private const float MinValue = 0f;
    private const float MaxValue = 1f;
    private const float ErrorRate = 0.005f;

    [SerializeField] private Slider _slider;
    [SerializeField] private SpriteRenderer _vampireAura;
    [SerializeField] private VampirismSkill _vampirismSkill;

    private WaitForSeconds _wait;
    private WaitForSeconds _waitForSlider;
    private float _delayForSlider = 0.02f;

    private void Awake()
    {
        _wait = new WaitForSeconds(_vampirismSkill.ActionTime);
        _waitForSlider = new WaitForSeconds(_delayForSlider);

        _vampireAura.transform.localScale *= (_vampirismSkill.Radius + _vampirismSkill.Radius);
    }

    private void OnEnable()
    {
        _vampirismSkill.UsedSkill += RenderSkill;
        _vampirismSkill.FinishedSkill += RenderCooldownSkill;
    }

    private void OnDisable()
    {
        _vampirismSkill.UsedSkill -= RenderSkill;
        _vampirismSkill.FinishedSkill -= RenderCooldownSkill;
    }

    public void RenderSkill()
    {
        _vampireAura.gameObject.SetActive(true);

        StartCoroutine(ChangeSliderValue(MinValue, _vampirismSkill.ActionTime));
        StartCoroutine(DisableVampireAuraOverTime());
    }

    public void RenderCooldownSkill(float cooldown)
    {
        StartCoroutine(ChangeSliderValue(MaxValue, cooldown));
    }

    private IEnumerator ChangeSliderValue(float targetValue, float actionTime)
    {
        float time = MinValue;
        float step = Mathf.Abs((MaxValue) * _delayForSlider / actionTime);

        while (actionTime - time > ErrorRate)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, step);
            yield return _waitForSlider;
            time += _delayForSlider;
        }
    }

    private IEnumerator DisableVampireAuraOverTime()
    {
        yield return _wait;
        _vampireAura.gameObject.SetActive(false);
    }
}