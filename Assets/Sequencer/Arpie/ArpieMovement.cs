using UnityEngine;

namespace Arpie {

class ArpieMovement : MonoBehaviour
{
    [SerializeField] float _stepPerMin = 200;
    [SerializeField] float _quantization = 0.8f;

    public float Interval { get; set; }

    float _phase;
    int _step;
    bool _delaying;

    System.Collections.IEnumerator Start()
    {
        // Pause at start.
        _delaying = true;

        // Quantize the current time.
        var t0 = Time.time;
        var step0 = 60 * (Mathf.FloorToInt(_stepPerMin * t0 / 60) + 1);
        var waitFor = step0 / _stepPerMin;

        // Apply the quantization ratio.
        waitFor = Mathf.Lerp(t0, waitFor, _quantization);

        // Wait for the timing.
        while (Time.time < waitFor) yield return null;

        // Boom!
        _delaying = false;
    }

    void Update()
    {
        if (_delaying) return;

        var dt = Time.deltaTime;
        _phase += _stepPerMin * dt / (60 * Interval);

        var pos = transform.localPosition;
        pos.y = Interval * Mathf.Abs(Mathf.Cos(_phase * Mathf.PI));
        transform.localPosition = pos;

        var stepNew = Mathf.FloorToInt(_phase + 0.5f);

        if (stepNew > _step)
        {
            transform.parent.gameObject.BroadcastMessage("KeyOn");
            _step = stepNew;
        }
    }

    void RemoveArpies()
      => enabled = false;
}

} // namespace Arpie
