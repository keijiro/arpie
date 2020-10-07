using UnityEngine;

namespace Arpie {

class CameraAdjuster : MonoBehaviour
{
    void Awake()
    {
        var ratio = (float)Screen.width / Screen.height;
        var camera = GetComponent<Camera>();
        var pos = transform.localPosition;

        if (ratio < 1.48f)
        {
            // Probably iPad
            camera.orthographicSize = 6f;
            pos.y = 0.75f;
        }
        else if (ratio > 1.58f)
        {
            // iPhone 5 and wide screen devices
            camera.orthographicSize = 5f;
            pos.y = -0.2f;
        }

        transform.localPosition = pos;
    }
}

} // namespace Arpie
