namespace Arpie.Synth {

class Envelope
{
    public float Attack;
    public float Release;
    public float Current;
    public float Amplifier;

    float _delta;

    public float Level => Current * Amplifier;

    public Envelope()
    {
        Attack = 0.018f;
        Release = 0.2f;
        Amplifier = 1f;
    }

    public void KeyOn()
      => _delta = 1 / (Attack * Config.SampleRate);

    public void Update()
    {
        if (_delta > 0)
        {
            Current += _delta;
            if (Current >= 1)
            {
                Current = 1;
                _delta = -1 / (Release * Config.SampleRate);
            }
        }
        else
        {
            Current = UnityEngine.Mathf.Max(Current + _delta, 0);
        }
    }
}

} // namespace Arpie.Synth
