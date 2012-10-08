#pragma strict

private var vibe = 0.0;
private var initialScale = 1.0;

function Start() {
	initialScale = transform.localScale.x;
}

function SetColor(index : int) {
	if (index & 1) {
		renderer.material.color = Color(0.9, 0.9, 0.9);
	} else {
		renderer.material.color = Color(0.7, 0.7, 0.7);
	}
}

function Update() {
	transform.localScale = Vector3.one * initialScale * (1.0 + 0.2 * vibe * Mathf.Sin(40.0 * Time.time));
	vibe = ExpEase.Out(vibe, 0.0, -8.0);
}

function KeyOn() {
	vibe = 1.0;
}