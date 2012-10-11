#pragma strict

class Oscillator {
	private var cx = 0.0;
	private var step = 0.0;

	static private var kPi2 = 6.28318530718;
	
	function SetNote(note : int) {
		var freq = 440.0 * Mathf.Pow(2.0, 1.0 * (note - 69) / 12.0);
		step = freq / SynthConfig.kSampleRate;
	}
	
	function Run() {
		cx += step;
		cx -= Mathf.Floor(cx);
		return Mathf.Sin(kPi2 * cx);
	}
}
