#pragma strict

var interval = 1.0;

private var time = 0.0;
private var step = 0;

private var running = false;

function Start() {
	var step = Mathf.FloorToInt(10.0 * Time.time / Mathf.PI);
	while (step == Mathf.FloorToInt(10.0 * Time.time / Mathf.PI)) yield;
	running = true;
}

function Update() {
	if (!running) return;

	time += Time.deltaTime;

	var param = 10.0 * time / interval;
	var currentStep = Mathf.FloorToInt((param + 0.5 * Mathf.PI) / Mathf.PI);

	transform.localPosition.y = interval * Mathf.Abs(Mathf.Cos(param));

	if (currentStep > step) {
		transform.parent.gameObject.BroadcastMessage("KeyOn");
		step = currentStep;
	}
}
