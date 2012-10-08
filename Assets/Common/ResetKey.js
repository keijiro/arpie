#pragma strict

function OnGUI() {
	if (GUI.Button(Rect(Screen.width - 120, Screen.height - 48, 120, 48), "RESET")) {
		Application.LoadLevel(1);
	}
}