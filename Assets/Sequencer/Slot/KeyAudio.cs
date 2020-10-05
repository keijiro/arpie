using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class KeyAudio : MonoBehaviour
{
    private static int baseNote;
    private static float clipLength;
    private static int[][] scales;
     // Pentatonic
     // Diatonic
     // Pentatonic + IIV
     // Major blues
     // Ryukyu
    private AudioClip clip;
    public virtual void SetKey(int scaleIndex, int degree)
    {
        int[] intervals = KeyAudio.scales[scaleIndex % KeyAudio.scales.Length];
        degree = degree + (intervals.Length - 2);
        int interval = intervals[degree % intervals.Length];
        int octave = degree / intervals.Length;
        Oscillator osc = new Oscillator();
        osc.SetNote((KeyAudio.baseNote + (octave * 12)) + interval);
        Envelope env = new Envelope();
        env.KeyOn();
        float[] samples = new float[(int)(SynthConfig.kSampleRate * KeyAudio.clipLength)];
        int i = 0;
        while (i < samples.Length)
        {
            samples[i] = osc.Run() * env.current;
            env.Update();
            i++;
        }
        this.clip = AudioClip.Create("note", (int) (SynthConfig.kSampleRate * KeyAudio.clipLength), 1, SynthConfig.kSampleRate, false, false);
        this.clip.SetData(samples, 0);
    }

    public virtual void KeyOn()
    {
        this.GetComponent<AudioSource>().PlayOneShot(this.clip);
    }

    static KeyAudio()
    {
        KeyAudio.baseNote = 60 - 12;
        KeyAudio.clipLength = 0.3f;
        KeyAudio.scales = new int[][] {new int[] {0, 2, 4, 7, 9}, new int[] {0, 2, 4, 5, 7, 9, 11}, new int[] {0, 2, 4, 7, 9, 11}, new int[] {0, 2, 3, 4, 7, 9}, new int[] {0, 4, 5, 7, 11}};
    }

}
