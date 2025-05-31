using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderHealthView : HealthView
{
    private const float Coefficient = 4f;
    private const float Delay = 0.01f;

    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothEffectTime = 1f;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(Delay);
    }

    protected override void Show(float value)
    {
        float sliderValue = value / Health.MaxCount;

        _coroutine = StartCoroutine(ChangeValueOfSlider(sliderValue));
    }

    private IEnumerator ChangeValueOfSlider(float targetValue)
    {
        float step = Mathf.Abs((targetValue - _slider.value) * Coefficient / _smoothEffectTime);

        while (_slider.value != targetValue)
        {
            yield return _wait;
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, step * Time.deltaTime);
        }
    }
}