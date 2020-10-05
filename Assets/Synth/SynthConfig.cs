using System.Collections;

[System.Serializable]
public class SynthConfig : object
{
    public static int kSampleRate;
    static SynthConfig()
    {
        SynthConfig.kSampleRate = 44100;
    }

}