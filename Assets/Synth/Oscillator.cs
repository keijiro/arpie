namespace Arpie.Synth {

class Oscillator
{
    float _cx;
    float _step;

    const float Pi2 = 6.28318530718f;

    public void SetNote(int note)
    {
        var freq = 440 * UnityEngine.Mathf.Pow(2, (note - 69) / 12.0f);
        _step = freq / Config.SampleRate;
    }

    public float Run()
    {
        _cx += _step;
        _cx -= UnityEngine.Mathf.Floor(_cx);
        return UnityEngine.Mathf.Sin(Pi2 * _cx);
    }
}

} // namespace Arpie.Synth
