using UnityEngine;

namespace Arpie {

class ArpieAnimation : MonoBehaviour
{
    Quaternion _initialRotation;
    float _freqX;
    float _freqY;
    float _freqS;

    void Start()
    {
        _initialRotation = transform.localRotation;
        _freqX = Random.Range(7.0f, 13.0f);
        _freqY = Random.Range(7.0f, 13.0f);
        _freqS = Random.Range(7.0f, 13.0f);
    }

    void Update()
    {
        var t = Time.time;

        transform.localRotation =
          _initialRotation *
          Quaternion.AngleAxis(10 * Mathf.Sin(_freqY * t), Vector3.up) *
          Quaternion.AngleAxis(10 * Mathf.Sin(_freqX * t), Vector3.right);

        transform.localScale =
          Vector3.one * (1 + 0.1f * Mathf.Sin(_freqS * t));
    }

    void RemoveArpies()
      => enabled = false;
}

} // namespace Arpie
