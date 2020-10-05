#pragma strict

class Envelope {
    var attack = 0.018;
    var release = 0.2;
    var current = 0.0;
    var amplifier = 1.0;

    private var delta = 0.0;

    function Envelope() {
    }

    function KeyOn() {
        delta = 1.0 / (attack * SynthConfig.kSampleRate);
    }

    function Update() {
        if (delta > 0.0) {
            current += delta;
            if (current >= 1.0) {
                current = 1.0;
                delta = -1.0 / (release * SynthConfig.kSampleRate);
            }
        } else {
            current = Mathf.Max(current + delta, 0.0);
        }
    }

    function GetLevel() {
        return current * amplifier;
    }
}
