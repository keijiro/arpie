using UnityEngine;

namespace Arpie {

class KeyAudio : MonoBehaviour
{
    static int _baseNote = 60 - 12;
    static float _clipLength = 0.3f;

    static int [][] _scales = new int [][]
    {
        new [] {0, 2, 4, 7, 9},        // Pentatonic
        new [] {0, 2, 4, 5, 7, 9, 11}, // Diatonic
        new [] {0, 2, 4, 7, 9, 11},    // Pentatonic + IIV
        new [] {0, 2, 3, 4, 7, 9},     // Major blues
        new [] {0, 4, 5, 7, 11}        // Ryukyu
    };

    AudioClip _clip;
    AudioSource _source;

    void Start()
      => _source = GetComponent<AudioSource>();

    public void KeyOn()
      => _source.PlayOneShot(_clip);

    public void SetKey(int scaleIndex, int degree)
    {
        var intervals = _scales[scaleIndex % _scales.Length];

        degree += intervals.Length - 2;

        var interval = intervals[degree % intervals.Length];
        var octave = degree / intervals.Length;

        var osc = new Synth.Oscillator();
        osc.SetNote(_baseNote + octave * 12 + interval);

        var env = new Synth.Envelope();
        env.KeyOn();

        var samples = new float[(int)(Synth.Config.SampleRate * _clipLength)];

        for (var i = 0; i < samples.Length; i++)
        {
            samples[i] = osc.Run() * env.Current;
            env.Update();
        }

        if (_clip != null) Destroy(_clip);
        _clip = AudioClip.Create("note", samples.Length, 1, Synth.Config.SampleRate, false); 
        _clip.SetData(samples, 0);
    }
}

} // namespace Arpie
