using UnityEngine;

namespace Arpie {

class PostFX : MonoBehaviour
{
    [SerializeField] float _vignette = 0.65f;
    [SerializeField] float _fadingDelay = 0.1f;
    [SerializeField] float _fadingSpeed = 0.5f;

    [HideInInspector, SerializeField] Shader _shader = null;
    [HideInInspector, SerializeField] Texture2D _vignetteTexture = null;

    Material _material;

    float _time;

    float Fading => 1 - (_time - _fadingDelay) * _fadingSpeed;

    void OnDestroy()
    {
        if (_material != null) Destroy(_material);
    }

    void Update()
    {
        _time += Time.deltaTime;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_material == null)
            _material = new Material(_shader);

        _material.SetTexture("_VignetteTex", _vignetteTexture);
        _material.SetFloat("_Vignette", _vignette);
        _material.SetFloat("_Fading", Mathf.Clamp01(Fading));

        Graphics.Blit(source, destination, _material, 0);
    }
}

} // namespace Arpie
