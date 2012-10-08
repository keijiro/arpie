#pragma strict

function OnGUI() {
	if (GUILayout.Button("RESET")) {
		Application.LoadLevel(0);
	}
}