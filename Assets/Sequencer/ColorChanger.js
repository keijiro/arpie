#pragma strict

private static var colors = [
	Color(0.5, 0.5, 1.0),
	Color(0.5, 1.0, 0.5),
	Color(0.5, 1.0, 1.0),
	Color(1.0, 0.5, 0.5),
	Color(1.0, 0.5, 1.0),
	Color(1.0, 1.0, 0.5)
];

static var counter = 0;

function Start() {
	renderer.material.color = colors[counter];
	if (++counter == colors.Length) counter = 0;
}
