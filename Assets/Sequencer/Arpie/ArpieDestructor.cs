using UnityEngine;

namespace Arpie {

class ArpieDestructor : MonoBehaviour
{
    [SerializeField] GameObject _explosion = null;

    System.Collections.IEnumerator RemoveArpies()
    {
        var initPosition = transform.position;

        for (var t = 0.0f; t < 0.2f; t += Time.deltaTime)
        {
            var param = 1 - t * 5;
            var phase = 0.75f * Mathf.PI * param;
            var vibe = param * param * param * param * 0.4f;
            var scale = (1 - Mathf.Abs(Mathf.Cos(phase))) * 1.2f + 0.5f;

            transform.position = initPosition + Random.onUnitSphere * vibe;
            transform.localScale = Vector3.one * scale;

            yield return null;
        }

        for (var t = 0.0f; t < 0.03f; t += Time.deltaTime)
            yield return null;

        Instantiate(_explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

} // namespace Arpie
