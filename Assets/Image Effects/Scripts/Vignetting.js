#pragma strict

@script ExecuteInEditMode
@script RequireComponent(Camera)
@script AddComponentMenu("Image Effects/Vignetting")

class Vignetting extends PostEffectsBase {
    @Range(0.0, 1.5) var vignetteIntensity = 0.375;

    @HideInInspector var shader : Shader;
    @HideInInspector var gradTexture : Texture2D;

    private var material : Material;

    function CheckResources() {
        material = CheckShaderAndCreateMaterial(shader, material);
        return CheckSupport();
    }

    function OnRenderImage(source : RenderTexture, destination : RenderTexture) {
        if (!CheckResources()) {
            ReportAutoDisable();
            Graphics.Blit(source, destination);
            return;
        }

        material.SetTexture("grad_texture", gradTexture);
        material.SetFloat("vignette_intensity", vignetteIntensity);

        Graphics.Blit(source, destination, material);
    }
}
