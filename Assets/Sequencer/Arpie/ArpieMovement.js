#pragma strict

var interval = 1.0;

private var time = 0.0;
private var step = 0;

function Start() {
	interval = Random.Range(2, 10);
}

function Update() {
	time += Time.deltaTime;

	var param = 10.0 * time / interval;
	var currentStep = Mathf.FloorToInt((param + 0.5 * Mathf.PI) / Mathf.PI);

	transform.localPosition.y = interval * Mathf.Abs(Mathf.Cos(param));

	if (currentStep > step) {
		transform.parent.gameObject.BroadcastMessage("KeyOn");
		step = currentStep;
	}
}
