#pragma strict

@script ExecuteInEditMode
@script RequireComponent(Camera)
@script AddComponentMenu("Image Effects/Blackout")

class Blackout extends PostEffectsBase {
    @Range(0.0, 1.0) var delay = 0.2;
    @Range(0.1, 5.0) var speed = 1.0;
    @HideInInspector var shader : Shader;

    private var level = 1.0;
    private var material : Material;

    function CheckResources() {
        material = CheckShaderAndCreateMaterial(shader, material);
        return CheckSupport();
    }

    function Update() {
        if (delay > 0) delay -= Time.deltaTime;
        if (delay <= 0) {
            level -= Time.deltaTime / speed;
            if (level < 0) enabled = false;
        }
    }

    function OnRenderImage(source : RenderTexture, destination : RenderTexture) {
        if (!CheckResources()) {
            ReportAutoDisable();
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("level", level);
        Graphics.Blit(source, destination, material, 0);
    }
}
