using UnityEngine;

namespace Arpie {

class KeyCube : MonoBehaviour
{
    static Color [] BaseColors = new []
    {
        new Color(1, 1, 1),         // Pentatonic
        new Color(0.7f, 1, 0.7f),   // Diatonic
        new Color(0.7f, 1, 1),      // Pentatonic + IIV
        new Color(0.7f, 0.7f, 1),   // Major blues
        new Color(1, 1, 0.7f)       // Ryukyu
    };

    static int ColorKey = Shader.PropertyToID("_Color");

    Transform _mesh;
    Renderer _renderer;
    MaterialPropertyBlock _overrides;

    float _vibe;
    float _spin;

    void Awake()
    {
        _mesh = transform.Find("Mesh");
        _renderer = _mesh.GetComponent<Renderer>();
        _overrides = new MaterialPropertyBlock();
    }

    public void SetColor(int scaleIndex, int degree)
    {
        var value = (degree & 1) != 0 ? 0.7f : 0.9f;
        var color = BaseColors[scaleIndex % BaseColors.Length] * value;
        _overrides.SetColor(ColorKey, color);
        _renderer.SetPropertyBlock(_overrides);
    }

    void Update()
    {
        var scale = 1 + 0.2f * _vibe * Mathf.Sin(40 * Time.time);
        scale *= 1 - 0.5f * _spin;

        _mesh.localRotation = Quaternion.AngleAxis(_spin * 360, Vector3.up);
        _mesh.localScale = Vector3.one * scale;

        _vibe = ExpEase.Out(_vibe, 0, -8);
        _spin = ExpEase.Out(_spin, 0, -8);
    }

    void KeyOn()
      => _vibe = 1;

    void RemoveArpies()
      => (_vibe, _spin) = (1, 1);
}

} // namespace Arpie
