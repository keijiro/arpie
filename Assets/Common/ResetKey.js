#pragma strict

private static var resetCount = 0;

function OnGUI() {
	if (GUI.Button(Rect(Screen.width - 120, Screen.height - 48, 120, 48), "RESET")) {
		resetCount++;
		StartCoroutine(function(){
			var slots = GameObject.FindGameObjectsWithTag("Slot");
			for (var i = 0; i < slots.Length; i++) {
				var slot = slots[i];
				slot.BroadcastMessage("RemoveArpies");
				slot.GetComponentInChildren.<KeyCube>().SetColor(resetCount, i);
				slot.GetComponentInChildren.<KeyAudio>().SetKey(resetCount, i);
				yield WaitForSeconds(0.03);
			}
		}());
	}
}
