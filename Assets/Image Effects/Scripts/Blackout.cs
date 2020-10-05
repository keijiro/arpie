using UnityEngine;
using System.Collections;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
[UnityEngine.AddComponentMenu("Image Effects/Blackout")]
public partial class Blackout : PostEffectsBase
{
    [UnityEngine.Range(0f, 1f)]
    public float delay;
    [UnityEngine.Range(0.1f, 5f)]
    public float speed;
    [UnityEngine.HideInInspector]
    public Shader shader;
    private float level;
    private Material material;
    public override bool CheckResources()
    {
        this.material = this.CheckShaderAndCreateMaterial(this.shader, this.material);
        return this.CheckSupport();
    }

    public virtual void Update()
    {
        if (this.delay > 0)
        {
            this.delay = this.delay - Time.deltaTime;
        }
        if (this.delay <= 0)
        {
            this.level = this.level - (Time.deltaTime / this.speed);
            if (this.level < 0)
            {
                this.enabled = false;
            }
        }
    }

    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!this.CheckResources())
        {
            this.ReportAutoDisable();
            Graphics.Blit(source, destination);
            return;
        }
        this.material.SetFloat("level", this.level);
        Graphics.Blit(source, destination, this.material, 0);
    }

    public Blackout()
    {
        this.delay = 0.2f;
        this.speed = 1f;
        this.level = 1f;
    }

}