#pragma strict

var numberOfKeys = 6;
var slotPrefab : GameObject;

function Start() {
	for (var i = 0; i < numberOfKeys; i++) {
		var position = transform.position + Vector3.right * i;
		var slot = Instantiate(slotPrefab, position, Quaternion.identity) as GameObject;
		slot.GetComponent.<SlotController>().degree = i;
	}
	Destroy(gameObject);
}
