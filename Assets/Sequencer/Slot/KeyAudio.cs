using UnityEngine;

namespace Arpie {

class KeyAudio : MonoBehaviour
{
    static int BaseNote = 60 - 12;
    static float Duration = 0.3f;

    static int [][] Scales = new int [][]
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

    void KeyOn()
      => _source.PlayOneShot(_clip);

    public void SetKey(int scaleIndex, int degree)
    {
        var intervals = Scales[scaleIndex % Scales.Length];

        degree += intervals.Length - 2;

        var interval = intervals[degree % intervals.Length];
        var octave = degree / intervals.Length;

        var osc = Synth.Oscillator.Note(BaseNote + octave * 12 + interval);
        var env = Synth.Envelope.Default();

        var samples = new float[(int)(Synth.Config.SampleRate * Duration)];

        for (var i = 0; i < samples.Length; i++)
            samples[i] = osc.Run() * env.Run();

        if (_clip != null) Destroy(_clip);
        _clip = AudioClip.Create("note", samples.Length, 1, Synth.Config.SampleRate, false); 
        _clip.SetData(samples, 0);
    }
}

} // namespace Arpie
