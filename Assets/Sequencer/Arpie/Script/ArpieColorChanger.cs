using UnityEngine;

namespace Arpie {

class ArpieColorChanger : MonoBehaviour
{
    [SerializeField] Color[] _colors = null;

    static MaterialPropertyBlock[] _overrides;
    static int _counter;

    void Awake()
    {
        if (_overrides == null)
        {
            _overrides = new MaterialPropertyBlock[_colors.Length];

            var colorKey = Shader.PropertyToID("_Color");

            for (var i = 0; i < _colors.Length; i++)
            {
                _overrides[i] = new MaterialPropertyBlock();
                _overrides[i].SetColor(colorKey, _colors[i]);
            }
        }

        GetComponent<Renderer>().SetPropertyBlock(_overrides[_counter]);
        _counter = (_counter + 1) % _colors.Length;
    }
}

} // namespace Arpie
