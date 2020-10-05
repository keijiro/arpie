using System.Collections;

[System.Serializable]
public class Envelope : object
{
    public float attack;
    public float release;
    public float current;
    public float amplifier;
    private float delta;
    public Envelope()
    {
        this.attack = 0.018f;
        this.release = 0.2f;
        this.amplifier = 1f;
    }

    public virtual void KeyOn()
    {
        this.delta = 1f / (this.attack * SynthConfig.kSampleRate);
    }

    public virtual void Update()
    {
        if (this.delta > 0f)
        {
            this.current = this.current + this.delta;
            if (this.current >= 1f)
            {
                this.current = 1f;
                this.delta = -1f / (this.release * SynthConfig.kSampleRate);
            }
        }
        else
        {
            this.current = UnityEngine.Mathf.Max(this.current + this.delta, 0f);
        }
    }

    public virtual float GetLevel()
    {
        return this.current * this.amplifier;
    }

}