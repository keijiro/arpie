#pragma strict

static private var baseNote = 60 - 12;
static private var clipLength = 0.3;

static private var scales = [
	[0, 2, 4, 7, 9],			// Pentatonic
	[0, 2, 4, 5, 7, 9, 11],		// Diatonic
	[0, 2, 4, 7, 9, 11],		// Pentatonic + IIV
	[0, 2, 3, 4, 7, 9],			// Major blues
	[0, 4, 5, 7, 11]			// Ryukyu
];

private var intervals = scales[0];
private var degree = 0;

function SetKey(scaleIndex : int, degree : int) {
	intervals = scales[scaleIndex % scales.Length];
	this.degree = degree;
}

function Start() {
	degree += intervals.Length - 2;
	var interval = intervals[degree % intervals.Length];
	var octave = degree / intervals.Length;

	var osc = Oscillator();
	osc.SetNote(baseNote + octave * 12 + interval);

	var env = Envelope();
	env.KeyOn();

	var samples = new float[SynthConfig.kSampleRate * clipLength];
	for (var i = 0; i < samples.Length; i++) {
		samples[i] = osc.Run() * env.current;
		env.Update();
	}

	audio.clip = AudioClip.Create("note", SynthConfig.kSampleRate * clipLength, 1, SynthConfig.kSampleRate, false, false);
	audio.clip.SetData(samples, 0);
}

function KeyOn() {
	audio.PlayOneShot(audio.clip);
}
