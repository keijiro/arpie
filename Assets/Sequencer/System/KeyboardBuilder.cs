using UnityEngine;

namespace Arpie {

class KeyboardBuilder : MonoBehaviour
{
    [SerializeField] int _numberOfKeys = 16;
    [SerializeField] float _panning = 1.4f;
    [SerializeField] float _volume = 0.35f;
    [SerializeField] float _highDecay = 0.08f;
    [SerializeField] GameObject _slotPrefab = null;

    void Start()
    {
        for (var i = 0; i < _numberOfKeys; i++)
        {
            var pos = transform.position + (Vector3.right * i);

            var slot = Instantiate(_slotPrefab, pos, Quaternion.identity);
            slot.GetComponentInChildren<KeyCube>().SetColor(0, i);
            slot.GetComponentInChildren<KeyAudio>().SetKey(0, i);

            var source = slot.GetComponentInChildren<AudioSource>();
            source.panStereo = _panning * ((float)i / _numberOfKeys - 0.5f);
            source.volume = _volume - _highDecay * i / _numberOfKeys;
        }

        Destroy(gameObject);
    }
}

} // namespace Arpie
