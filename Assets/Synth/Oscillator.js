#pragma strict

class Oscillator {
	var multiplier = 4.0;
	var modulation = 0.0;

	private var mx = 0.0;
	private var cx = 0.0;
	private var step = 0.0;

	static private var kPi = 3.14159265359;
	static private var kPi2 = 6.28318530718;
	static private var k4dPi = 1.27323954474;
	static private var k4dPiPi = 0.40528473456;

	// http://devmaster.net/forums/topic/4648-fast-and-accurate-sinecosine/
	static private function fast_sin(x : float) {
		x -= kPi;
		x = k4dPi * x - k4dPiPi * x * Mathf.Abs(x);
        return -0.225 * (x * Mathf.Abs(x) - x) - x;
	}
	
	function SetNote(note : int) {
		var freq = 440.0 * Mathf.Pow(2.0, 1.0 * (note - 69) / 12.0);
		step = freq / SynthConfig.kSampleRate;
	}
	
	function Run() {
		mx += step * multiplier;
		cx += step;
		mx -= Mathf.Floor(mx);
		cx -= Mathf.Floor(cx);
		var x = cx + modulation * fast_sin(kPi2 * mx);
		x -= Mathf.Floor(x);
		return fast_sin(kPi2 * x);
	}
}
