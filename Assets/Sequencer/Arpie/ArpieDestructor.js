#pragma strict

var explosion : GameObject;

private var initPosition : Vector3;
private var time = 1.0;

function Update() {
	time -= Time.deltaTime * 5.0;
	if (time > 0.0) {
		transform.position = initPosition + Random.onUnitSphere * time * time * time * time * 0.4;
		transform.localScale = Vector3.one * ((1.0 - Mathf.Abs(Mathf.Cos(0.75 * Mathf.PI * time))) * 1.2 + 0.5);
	} else if (time < -0.2) {
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}

function RemoveArpies() {
	initPosition = transform.position;
	enabled = true;
}
