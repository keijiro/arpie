using UnityEngine;

namespace Arpie {

class ResetButton : MonoBehaviour
{
    static int _resetCount;

    GameObject [] _slots;
    float _initialScale;
    float _vibe;

    void Start()
    {
        _slots = GameObject.FindGameObjectsWithTag("Slot");
        _initialScale = transform.localScale.x;
    }

    void Update()
    {
        _vibe = ExpEase.Out(_vibe, 0, -8);

        var scale = 1 + 0.5f * _vibe * Mathf.Sin(Time.time * 30);
        transform.localScale = Vector3.one * _initialScale * scale;
    }

    System.Collections.IEnumerator DoReset()
    {
        _vibe = 1;
        _resetCount++;

        var interval = new WaitForSeconds(0.03f);

        for (var i = 0; i < _slots.Length; i++)
        {
            var slot = _slots[i];
            slot.BroadcastMessage("RemoveArpies");
            slot.GetComponentInChildren<KeyCube>().SetColor(_resetCount, i);
            slot.GetComponentInChildren<KeyAudio>().SetKey(_resetCount, i);
            yield return interval;
        }
    }
}

} // namespace Arpie
