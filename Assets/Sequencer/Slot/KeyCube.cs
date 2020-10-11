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

    static MaterialPropertyBlock[] _overrides;

    Transform _mesh;
    Renderer _renderer;

    float _vibe;
    float _spin;

    void Awake()
    {
        _mesh = transform.Find("Mesh");
        _renderer = _mesh.GetComponent<Renderer>();

        if (_overrides == null)
        {
            _overrides = new MaterialPropertyBlock[BaseColors.Length * 2];

            var colorKey = Shader.PropertyToID("_Color");

            for (var i = 0; i < BaseColors.Length * 2; i++)
            {
                var color = BaseColors[i / 2] * (i % 2 != 0 ? 0.7f : 0.9f);
                _overrides[i] = new MaterialPropertyBlock();
                _overrides[i].SetColor(colorKey, color);
            }
        }
    }

    public void SetColor(int scaleIndex, int degree)
    {
        var idx = (scaleIndex % BaseColors.Length) * 2 + degree % 2;
        _renderer.SetPropertyBlock(_overrides[idx]);
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
