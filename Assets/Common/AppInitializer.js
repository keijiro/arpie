#pragma strict

function Start() {
#if UNITY_IPHONE

    if (SystemInfo.processorCount == 1) {
	    SynthConfig.kSampleRate = AudioSettings.outputSampleRate;
        QualitySettings.SetQualityLevel(1);
    } else {
	    AudioSettings.outputSampleRate = SynthConfig.kSampleRate = 44100;
        QualitySettings.SetQualityLevel(2);
    }
    Application.targetFrameRate = 60;

#elif UNITY_ANDROID

	QualitySettings.SetQualityLevel(1);
	SynthConfig.kSampleRate = AudioSettings.outputSampleRate;

#endif

	yield;

	Application.LoadLevel(1);
}
