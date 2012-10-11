#pragma strict

var numberOfKeys = 6;
var slotPrefab : GameObject;

private static var counter = 0;

function Start() {
	for (var i = 0; i < numberOfKeys; i++) {
		var position = transform.position + Vector3.right * i;
		var slot = Instantiate(slotPrefab, position, Quaternion.identity) as GameObject;
		slot.GetComponentInChildren.<KeyCube>().SetColor(counter, i);
		slot.GetComponentInChildren.<KeyAudio>().SetKey(counter, i);
		slot.GetComponentInChildren.<AudioSource>().pan = 2.0 * i / numberOfKeys - 1.0;
	}
	counter++;
	Destroy(gameObject);
}
