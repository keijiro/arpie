#pragma strict

var colors : Color[];

static var counter = 0;

function Start() {
	renderer.material.color = colors[counter];
	if (++counter == colors.Length) counter = 0;
}
