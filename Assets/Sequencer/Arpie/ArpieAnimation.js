#pragma strict

private var initialRotation : Quaternion;
private var freq_rx = 1.0;
private var freq_ry = 1.0;
private var freq_s = 1.0;

function Start() {
	initialRotation = transform.localRotation;
	freq_rx = Random.Range(7.0, 13.0);
	freq_ry = Random.Range(7.0, 13.0);
	freq_s = Random.Range(7.0, 13.0);
}

function Update () {
	transform.localRotation =
		initialRotation *
		Quaternion.AngleAxis(10.0 * Mathf.Sin(Time.time * freq_ry), Vector3.up) *
		Quaternion.AngleAxis(10.0 * Mathf.Sin(Time.time * freq_rx), Vector3.right);
	transform.localScale = Vector3.one * (1.0 + 0.1 * Mathf.Sin(freq_s * Time.time));
}

function RemoveArpies() {
	enabled = false;
}
