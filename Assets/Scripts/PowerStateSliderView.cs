using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerStateSliderView : MonoBehaviour
{
	[SerializeField] private Slider slider;
	private bool suppressTrigger;

	public event Action<Power> StateChanged;

	private void Start()
	{
		slider.onValueChanged.AddListener(OnSliderValueChanged);
	}

	private void OnSliderValueChanged(float value)
	{
		if (suppressTrigger || StateChanged == null) return;
		StateChanged((Power) value);
	}

	public void SetState(Power newState, bool shouldSuppressTrigger = true)
	{
		suppressTrigger = shouldSuppressTrigger;
		slider.value = (float) newState;
		suppressTrigger = false;
	}
}