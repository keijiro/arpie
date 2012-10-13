#pragma strict

private static var resetCount = 0;

private var initialScale = 1.0;
private var vibe = 0.0;

function Start() {
	initialScale = transform.localScale.x;
}

function Update() {
	var param = vibe * Mathf.Sin(Time.time * 30.0);
	transform.localScale = Vector3.one * (initialScale * (1.0 + 0.5 * param));
	vibe = ExpEase.Out(vibe, 0.0, -8.0);
}

function DoReset() {
	vibe = 1.0;
	resetCount++;

	var slots = GameObject.FindGameObjectsWithTag("Slot");
	for (var i = 0; i < slots.Length; i++) {
		var slot = slots[i];
		slot.BroadcastMessage("RemoveArpies");
		slot.GetComponentInChildren.<KeyCube>().SetColor(resetCount, i);
		slot.GetComponentInChildren.<KeyAudio>().SetKey(resetCount, i);
		yield WaitForSeconds(0.03);
	}
}
