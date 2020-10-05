using System.Collections;

[System.Serializable]
public class Oscillator : object
{
    private float cx;
    private float step;
    private static float kPi2;
    public virtual void SetNote(int note)
    {
        float freq = 440f * UnityEngine.Mathf.Pow(2f, (1f * (note - 69)) / 12f);
        this.step = freq / SynthConfig.kSampleRate;
    }

    public virtual float Run()
    {
        this.cx = this.cx + this.step;
        this.cx = this.cx - UnityEngine.Mathf.Floor(this.cx);
        return UnityEngine.Mathf.Sin(Oscillator.kPi2 * this.cx);
    }

    static Oscillator()
    {
        Oscillator.kPi2 = 6.28318530718f;
    }

}