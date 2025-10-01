using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GazAnalyzer : MonoBehaviour
{
    [SerializeField] private float _timeToSwitchPower;
    [SerializeField] private Image _powerImage;
    [SerializeField] private TMP_Text _probeDistanceText;
    [SerializeField] private TMP_Text _analyzerDistanceText;
    [SerializeField] private DistanceToDangerZone _probe;
    [SerializeField] private DistanceToDangerZone _analyzer;
    [SerializeField] private GameObject _screen;

    private bool _isPowerOn;
    private Tween _powerTween;
    private Coroutine _switchPowerRoutine;
    private WaitForSeconds _waitForSwitch;

    private const float FULL_POWEL_FILL = 1f;

    private void Awake()
    {
        _waitForSwitch = new(_timeToSwitchPower);
    }

    private IEnumerator SwitchPowerRoutine()
    {
        float powerImageFillAmount = _isPowerOn ? 0 : FULL_POWEL_FILL;
        _powerTween = _powerImage.DOFillAmount(powerImageFillAmount, _timeToSwitchPower).SetEase(Ease.Linear);
        yield return _waitForSwitch;
        _isPowerOn = !_isPowerOn;
        _screen.SetActive(_isPowerOn);
    }

    public void OnPowerButtonPressed()
    {
        _switchPowerRoutine = StartCoroutine(SwitchPowerRoutine());
    }

    public void OnPowerButtonReleased()
    {
        if (_switchPowerRoutine != null)
        {
            _powerTween.Rewind();
            StopCoroutine(_switchPowerRoutine);
        }
    }

    private void Update()
    {
        if (!_isPowerOn)
            return;

        _probeDistanceText.text = _probe.DistanceToNearestDangerZone;
        _analyzerDistanceText.text = _analyzer.DistanceToNearestDangerZone;
    }
}