#pragma strict

var stepPerMin = 200.0;
var interval = 1.0;
var quantization = 0.8;

private var phase = 0.0;
private var step = 0;

private var delaying = true;

function Start() {
	// Quantize the current time.
	var waitFor = 60.0 * (Mathf.FloorToInt(stepPerMin * Time.time / 60.0) + 1) / stepPerMin;
	// Apply the quantization ratio.
	waitFor = Mathf.Lerp(Time.time, waitFor, quantization);
	// Wait for the timing.
	while (Time.time < waitFor) yield;
	// Start!
	delaying = false;
}

function Update() {
	if (delaying) return;

	phase += stepPerMin * Time.deltaTime / (60.0 * interval);
	transform.localPosition.y = interval * Mathf.Abs(Mathf.Cos(phase * Mathf.PI));

	var stepNew = Mathf.FloorToInt(phase + 0.5);
	if (stepNew > step) {
		transform.parent.gameObject.BroadcastMessage("KeyOn");
		step = stepNew;
	}
}
