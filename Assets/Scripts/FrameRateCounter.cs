using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    public enum DisplayMode {FPS, MS }


    [SerializeField] private TextMeshProUGUI _display;
    [SerializeField, Range(0.1f, 2f)] private float _sampleDuration = 1f;
    [SerializeField] private DisplayMode _displayMode = DisplayMode.FPS;

    private int _frames;
    private float _duration;
    private float _worstDuration;
    private float _bestDuration = float.MaxValue;

    private void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        _frames += 1;
        _duration += frameDuration;

        if (frameDuration < _bestDuration)
        {
            _bestDuration = frameDuration;
        }
        if (frameDuration > _worstDuration)
        {
            _worstDuration = frameDuration;
        }
        if (_duration >= _sampleDuration)
        {
            if (_displayMode == DisplayMode.FPS)
            {
                _display.SetText("FPS\n{0:0}\n{1:0}\n{2:0}", 1f / _bestDuration, _frames / _duration, 1f / _worstDuration);
            }
            else
            {
                _display.SetText("MS\n{0:1}\n{1:1}\n{2:1}", 1000f * _bestDuration,  _duration / _frames, 1000f * _worstDuration);
            }
            _duration = 0f;
            _frames = 0;
            _bestDuration = float.MaxValue;
            _worstDuration = 0f;
        }

    }
}
