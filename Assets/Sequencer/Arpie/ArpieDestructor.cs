using UnityEngine;

namespace Arpie {

class ArpieDestructor : MonoBehaviour
{
    [SerializeField] GameObject _explosion = null;

    Vector3 _initPosition;
    float _time = 1;

    void Update()
    {
        _time -= Time.deltaTime * 5;

        if (_time > 0)
        {
            var vibe = _time * _time * _time * _time * 0.4f;

            var scale = Mathf.Abs(Mathf.Cos(0.75f * Mathf.PI * _time));
            scale = (1 - scale) * 1.2f + 0.5f;

            transform.position = _initPosition + Random.onUnitSphere * vibe;
            transform.localScale = Vector3.one * scale;
        }
        else if (_time < -0.2f)
        {
            Instantiate(_explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void RemoveArpies()
    {
        _initPosition = transform.position;
        enabled = true;
    }
}

} // namespace Arpie
