#pragma strict

var audioClips : AudioClip[];
var slotPrefab : GameObject;

function Start() {
	for (var i = 0; i < audioClips.Length; i++) {
		var position = transform.position + Vector3.right * i;
		var slot = Instantiate(slotPrefab, position, Quaternion.identity) as GameObject;
		slot.GetComponent.<SlotController>().audio.clip = audioClips[i];
	}
	Destroy(gameObject);
}
