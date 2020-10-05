using UnityEngine;
using System.Collections;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Vignetting")]
public partial class Vignetting : PostEffectsBase
{
    [UnityEngine.Range(0f, 1.5f)]
    public float vignetteIntensity;
    [UnityEngine.HideInInspector]
    public Shader shader;
    [UnityEngine.HideInInspector]
    public Texture2D gradTexture;
    private Material material;
    public virtual void Awake()
    {
        this.enabled = QualitySettings.GetQualityLevel() > 1;
    }

    public override bool CheckResources()
    {
        this.material = this.CheckShaderAndCreateMaterial(this.shader, this.material);
        return this.CheckSupport();
    }

    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!this.CheckResources())
        {
            this.ReportAutoDisable();
            Graphics.Blit(source, destination);
            return;
        }
        this.material.SetTexture("grad_texture", this.gradTexture);
        this.material.SetFloat("vignette_intensity", this.vignetteIntensity);
        Graphics.Blit(source, destination, this.material);
    }

    public Vignetting()
    {
        this.vignetteIntensity = 0.375f;
    }

}