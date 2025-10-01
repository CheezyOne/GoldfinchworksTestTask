using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GasAnalyzer : MonoBehaviour
{
    [SerializeField] private float _timeToSwitchPower;
    [SerializeField] private float _screenAppearTime;
    [SerializeField] private float _gasDangerDistance;
    [SerializeField] private Image _powerImage;
    [SerializeField] private Image _gasImage;
    [SerializeField] private TMP_Text _probeDistanceText;
    [SerializeField] private TMP_Text _analyzerDistanceText;
    [SerializeField] private DistanceToDangerZone _probe;
    [SerializeField] private DistanceToDangerZone _analyzer;
    [SerializeField] private CanvasGroup _screen;

    private bool _isPowerOn;
    private Tween _powerTween;
    private Coroutine _switchPowerRoutine;
    private WaitForSeconds _waitForSwitch;

    private const float FULL_POWER_FILL = 1f;
    private const float MAX_CANVAS_BRIGHT = 1f;

    private void Awake()
    {
        _waitForSwitch = new(_timeToSwitchPower);
    }

    private IEnumerator SwitchPowerRoutine()
    {
        _powerTween = _powerImage.DOFillAmount(_isPowerOn ? 0 : FULL_POWER_FILL, _timeToSwitchPower).SetEase(Ease.Linear);
        yield return _waitForSwitch;
        _isPowerOn = !_isPowerOn;
        _screen.DOFade(_isPowerOn ? MAX_CANVAS_BRIGHT : 0, _screenAppearTime);
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


        if(_analyzer.DistanceToNearestDangerZone < _gasDangerDistance)
            _gasImage.fillAmount = (_gasDangerDistance - _analyzer.DistanceToNearestDangerZone) / _gasDangerDistance;

        _probeDistanceText.text = _probe.DistanceToNearestDangerZoneText;
        _analyzerDistanceText.text = _analyzer.DistanceToNearestDangerZoneText;
    }
}