namespace Arpie.Synth {

struct Envelope
{
    const float Attack = 0.018f;
    const float Release = 0.2f;

    float _current;
    float _delta;

    static public Envelope Default()
      => new Envelope { _delta = 1 / (Attack * Config.SampleRate) };

    public float Run()
    {
        _current += _delta;

        if (_delta > 0 && _current >= 1)
            _delta = -1 / (Release * Config.SampleRate);

        return UnityEngine.Mathf.Clamp01(_current);
    }
}

} // namespace Arpie.Synth
