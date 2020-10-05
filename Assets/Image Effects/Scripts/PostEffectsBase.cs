using UnityEngine;
using System.Collections;

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(Camera))]
public partial class PostEffectsBase : MonoBehaviour
{
    protected bool supportHDRTextures;
    protected bool isSupported;
    public virtual Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
    {
        if (!s)
        {
            Debug.Log("Missing shader in " + this.ToString());
            this.enabled = false;
            return null;
        }
        if ((s.isSupported && m2Create) && (m2Create.shader == s))
        {
            return m2Create;
        }
        if (!s.isSupported)
        {
            this.NotSupported();
            Debug.LogError(((("The shader " + s.ToString()) + " on effect ") + this.ToString()) + " is not supported on this platform!");
            return null;
        }
        else
        {
            m2Create = new Material(s);
            m2Create.hideFlags = HideFlags.DontSave;
            if (m2Create)
            {
                return m2Create;
            }
            else
            {
                return null;
            }
        }
    }

    public virtual Material CreateMaterial(Shader s, Material m2Create)
    {
        if (!s)
        {
            Debug.Log("Missing shader in " + this.ToString());
            return null;
        }
        if ((m2Create && (m2Create.shader == s)) && s.isSupported)
        {
            return m2Create;
        }
        if (!s.isSupported)
        {
            return null;
        }
        else
        {
            m2Create = new Material(s);
            m2Create.hideFlags = HideFlags.DontSave;
            if (m2Create)
            {
                return m2Create;
            }
            else
            {
                return null;
            }
        }
    }

    public virtual void OnEnable()
    {
        this.isSupported = true;
    }

    // deprecated but needed for old effects to survive upgrade
    public virtual bool CheckSupport()
    {
        return this.CheckSupport(false);
    }

    public virtual bool CheckResources()
    {
        Debug.LogWarning(("CheckResources () for " + this.ToString()) + " should be overwritten.");
        return this.isSupported;
    }

    public virtual void Start()
    {
        this.CheckResources();
    }

    public virtual bool CheckSupport(bool needDepth)
    {
        this.isSupported = true;
        this.supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
        if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures)
        {
            this.NotSupported();
            return false;
        }
        if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
        {
            this.NotSupported();
            return false;
        }
        if (needDepth)
        {
            this.GetComponent<Camera>().depthTextureMode = this.GetComponent<Camera>().depthTextureMode | DepthTextureMode.Depth;
        }
        return true;
    }

    public virtual bool CheckSupport(bool needDepth, bool needHdr)
    {
        if (!this.CheckSupport(needDepth))
        {
            return false;
        }
        if (needHdr && !this.supportHDRTextures)
        {
            this.NotSupported();
            return false;
        }
        return true;
    }

    public virtual void ReportAutoDisable()
    {
        Debug.LogWarning(("The image effect " + this.ToString()) + " has been disabled as it's not supported on the current platform.");
    }

    // deprecated but needed for old effects to survive upgrading
    public virtual bool CheckShader(Shader s)
    {
        Debug.Log(((("The shader " + s.ToString()) + " on effect ") + this.ToString()) + " is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package.");
        if (!s.isSupported)
        {
            this.NotSupported();
            return false;
        }
        else
        {
            return false;
        }
    }

    public virtual void NotSupported()
    {
        this.enabled = false;
        this.isSupported = false;
        return;
    }

    public virtual void DrawBorder(RenderTexture dest, Material material)
    {
        float x1 = 0.0f;
        float x2 = 0.0f;
        float y1 = 0.0f;
        float y2 = 0.0f;
        float y1_ = 0.0f;
        float y2_ = 0.0f;
        RenderTexture.active = dest;
        bool invertY = true; // source.texelSize.y < 0.0f;
        // Set up the simple Matrix
        GL.PushMatrix();
        GL.LoadOrtho();
        int i = 0;
        while (i < material.passCount)
        {
            material.SetPass(i);
            if (invertY)
            {
                y1_ = 1f;
                y2_ = 0f;
            }
            else
            {
                y1_ = 0f;
                y2_ = 1f;
            }
            // left	        
            x1 = 0f;
            x2 = 0f + (1f / (dest.width * 1f));
            y1 = 0f;
            y2 = 1f;
            GL.Begin(GL.QUADS);
            GL.TexCoord2(0f, y1_);
            GL.Vertex3(x1, y1, 0.1f);
            GL.TexCoord2(1f, y1_);
            GL.Vertex3(x2, y1, 0.1f);
            GL.TexCoord2(1f, y2_);
            GL.Vertex3(x2, y2, 0.1f);
            GL.TexCoord2(0f, y2_);
            GL.Vertex3(x1, y2, 0.1f);
            // right
            x1 = 1f - (1f / (dest.width * 1f));
            x2 = 1f;
            y1 = 0f;
            y2 = 1f;
            GL.TexCoord2(0f, y1_);
            GL.Vertex3(x1, y1, 0.1f);
            GL.TexCoord2(1f, y1_);
            GL.Vertex3(x2, y1, 0.1f);
            GL.TexCoord2(1f, y2_);
            GL.Vertex3(x2, y2, 0.1f);
            GL.TexCoord2(0f, y2_);
            GL.Vertex3(x1, y2, 0.1f);
            // top
            x1 = 0f;
            x2 = 1f;
            y1 = 0f;
            y2 = 0f + (1f / (dest.height * 1f));
            GL.TexCoord2(0f, y1_);
            GL.Vertex3(x1, y1, 0.1f);
            GL.TexCoord2(1f, y1_);
            GL.Vertex3(x2, y1, 0.1f);
            GL.TexCoord2(1f, y2_);
            GL.Vertex3(x2, y2, 0.1f);
            GL.TexCoord2(0f, y2_);
            GL.Vertex3(x1, y2, 0.1f);
            // bottom
            x1 = 0f;
            x2 = 1f;
            y1 = 1f - (1f / (dest.height * 1f));
            y2 = 1f;
            GL.TexCoord2(0f, y1_);
            GL.Vertex3(x1, y1, 0.1f);
            GL.TexCoord2(1f, y1_);
            GL.Vertex3(x2, y1, 0.1f);
            GL.TexCoord2(1f, y2_);
            GL.Vertex3(x2, y2, 0.1f);
            GL.TexCoord2(0f, y2_);
            GL.Vertex3(x1, y2, 0.1f);
            GL.End();
            i++;
        }
        GL.PopMatrix();
    }

    public PostEffectsBase()
    {
        this.supportHDRTextures = true;
        this.isSupported = true;
    }

}