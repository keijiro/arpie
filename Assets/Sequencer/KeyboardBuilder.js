#pragma strict

var numberOfKeys = 16;
var panning = 1.4;
var volume = 0.35;
var highDecay = 0.08;
var slotPrefab : GameObject;

function Start() {
	for (var i = 0; i < numberOfKeys; i++) {
		var position = transform.position + Vector3.right * i;

		var slot = Instantiate(slotPrefab, position, Quaternion.identity) as GameObject;
		slot.GetComponentInChildren.<KeyCube>().SetColor(0, i);
		slot.GetComponentInChildren.<KeyAudio>().SetKey(0, i);
		
		var source = slot.GetComponentInChildren.<AudioSource>();
		source.pan = panning * (i / numberOfKeys - 0.5);
		source.volume = volume - highDecay * i / numberOfKeys;
	}
	Destroy(gameObject);
}
