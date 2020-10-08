using UnityEngine;

namespace Arpie {

class ArpieColorChanger : MonoBehaviour
{
    [SerializeField] Color[] _colors = null;

    static Material[] _materials;
    static int _counter;

    void Awake()
    {
        var renderer = GetComponent<Renderer>();

        if (_materials == null)
        {
            var sourceMaterial = renderer.material;

            _materials = new Material[_colors.Length];

            for (var i = 0; i < _colors.Length; i++)
            {
                _materials[i] = new Material(sourceMaterial);
                _materials[i].color = _colors[i];
            }
        }

        renderer.material = _materials[_counter];
        _counter = (_counter + 1) % _materials.Length;
    }
}

} // namespace Arpie
