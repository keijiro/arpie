#pragma strict

static var baseNote = 60 - 12;
static var clipLength = 0.3;

//static var intervals = [0, 2, 4, 7, 9];			// Pentatonic
//static var intervals = [0, 4, 5, 7, 11];			// Ryukyu
//static var intervals = [0, 2, 4, 6, 8, 10];		// Whole tone
//static var intervals = [0, 1, 4, 5, 7, 8, 10];		// Phrygian dominant
//static var intervals = [0, 1, 4, 5, 7, 8, 11];		// Double harmonic (Arabic)
//static var intervals = [0, 2, 4, 5, 6, 8, 10];		// Major Locrian (Arabic)
//static var intervals = [0, 2, 3, 6, 7, 8, 11];		// Hungarian minor
//static var intervals = [0, 2, 3, 4, 7, 9];		// Major blues
static var intervals = [0, 2, 4, 5, 7, 9, 11];	// Diatonic

var degree = 0;

function Start() {
	var osc = Oscillator();
	var env = Envelope();
	var bit = Bitcrusher();

	degree += intervals.Length - 2;
	osc.SetNote(baseNote + intervals[degree % intervals.Length] + degree / intervals.Length * 12);
	env.KeyOn();

	audio.clip = AudioClip.Create("note", SynthConfig.kSampleRate * clipLength, 1, SynthConfig.kSampleRate, false, false);
	var samples = new float[SynthConfig.kSampleRate * clipLength];
	for (var i = 0; i < samples.Length; i++) {
		samples[i] = bit.Run(osc.Run()) * env.current;
		env.Update();
	}
	audio.clip.SetData(samples, 0);
}

function KeyOn() {
	audio.PlayOneShot(audio.clip);
}
