#pragma strict

class Bitcrusher {
    private var counter = 0;
    private var sampled = 0.0;

    var interval = 4;
    var mix = 1.0;

    function Run(input : float) {
        if (counter++ % interval == 0) {
            sampled = input;
        }
        return input * (1.0 - mix) + sampled * mix;
    }
}
