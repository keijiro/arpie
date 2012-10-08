#pragma strict

var prefab : GameObject;

function Update() {
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
	for (var touch in Input.touches) {
		if (touch.phase == TouchPhase.Began) {
			SpawnIfHit(touch.position);
		}
	}
#else
	if (Input.GetMouseButtonDown(0)) {
		SpawnIfHit(Input.mousePosition);
	}
#endif
}

private function SpawnIfHit(screenCoord : Vector3) {
	var ray = Camera.main.ScreenPointToRay(screenCoord);
	var hit : RaycastHit;
	if (Physics.Raycast(ray, hit) && hit.collider) {
		var interval = int.Parse(hit.collider.name);
		var go = Instantiate(prefab);
		go.transform.parent = hit.transform.parent.parent;
		go.transform.localPosition = Vector3(0, interval, 0);
		go.GetComponent.<ArpieMovement>().interval = interval;
	}
}