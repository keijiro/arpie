#pragma strict

var arpiePrefab : GameObject;

function Update() {
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
	for (var touch in Input.touches) {
		if (touch.phase == TouchPhase.Began) {
			DoTouch(touch.position);
		}
	}
#else
	if (Input.GetMouseButtonDown(0)) {
		DoTouch(Input.mousePosition);
	}
#endif
}

private function DoTouch(screenCoord : Vector3) {
	var ray = Camera.main.ScreenPointToRay(screenCoord);
	var hit : RaycastHit;
	if (Physics.Raycast(ray, hit) && hit.collider) {
		if (hit.collider.name == "Key Cube") {
			hit.transform.parent.BroadcastMessage("RemoveArpies");
		} else if (hit.collider.name == "Reset Button") {
			hit.transform.SendMessage("DoReset");
		} else {
			SpawnWithHit(hit);
		}
	}
}

private function SpawnWithHit(hit : RaycastHit) {
	var interval = int.Parse(hit.collider.name);
	var go = Instantiate(arpiePrefab);
	go.transform.parent = hit.transform.parent.parent;
	go.transform.localPosition = Vector3(0, interval, 0);
	go.GetComponent.<ArpieMovement>().interval = interval;
}
