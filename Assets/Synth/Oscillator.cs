namespace Arpie.Synth {

struct Oscillator
{
    const float Pi2 = 6.28318530718f;

    static float NoteFreq(int note)
      => 440 * UnityEngine.Mathf.Pow(2, (note - 69) / 12.0f);

    float _step;
    float _phase;

    static public Oscillator Note(int note)
      => new Oscillator { _step = NoteFreq(note) / Config.SampleRate };

    public float Run()
    {
        _phase += _step;
        _phase -= UnityEngine.Mathf.Floor(_phase);
        return UnityEngine.Mathf.Sin(Pi2 * _phase);
    }
}

} // namespace Arpie.Synth
