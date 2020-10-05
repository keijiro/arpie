using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class CameraAdjuster : MonoBehaviour
{
    public virtual void Awake()
    {
        float ratio = (1f * Screen.width) / Screen.height;
        if (ratio < 1.48f)
        {
            // Probably iPad.
            this.GetComponent<Camera>().orthographicSize = 6f;

            {
                float _6 = 0.75f;
                Vector3 _7 = this.transform.localPosition;
                _7.y = _6;
                this.transform.localPosition = _7;
            }
        }
        else
        {
            if (ratio > 1.58f)
            {
                // iPhone 5 and wide screen devices.
                this.GetComponent<Camera>().orthographicSize = 5f;

                {
                    float _8 = -0.2f;
                    Vector3 _9 = this.transform.localPosition;
                    _9.y = _8;
                    this.transform.localPosition = _9;
                }
            }
        }
    }

}