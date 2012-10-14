#pragma strict

var noteType = 0;
var baseAlpha = 0.75;

private var state = "";
private var param = 0.0;

private var sx = 6.0;
private var sy = 6.0;

function Awake() {
	if (PlayerPrefs.GetInt("launch count") > 2) {
		Destroy(gameObject);
	}
}

function Start() {
	sx *= Random.Range(0.9, 1.0);
	sy *= Random.Range(0.9, 1.0);
	renderer.material.color.a = 0.0;

	if (noteType == 0) {
		yield WaitForSeconds(0.5);
	} else {
		while (TouchInput.spawnCount == 0) yield;
		yield WaitForSeconds(2.0);
	}

	state = "fade in";
	while (true) {
		if (noteType == 0) {
			if (TouchInput.spawnCount > 0) break;
		} else {
			if (TouchInput.cubeCount > 0) break;
		}
		yield;
	}

	state = "fade out";
	yield WaitForSeconds(0.5);
	Destroy(gameObject);
}

function Update() {
	if (state == "fade in") {
		param = Mathf.Min(param + Time.deltaTime * 2, 1.0);
	} else if (state == "fade out") {
		param = Mathf.Max(param - Time.deltaTime * 2, 0.0);
	}

	var dx = 0.05 * Mathf.Sin(Time.time * sx);
	var dy = 0.05 * Mathf.Sin(Time.time * sy);

	transform.localPosition = Vector3(dx, dy, 0.0);
	renderer.material.color.a = baseAlpha * param;
}
