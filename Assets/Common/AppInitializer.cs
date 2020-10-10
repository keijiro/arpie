using UnityEngine;

namespace Arpie {

class AppInitializer : MonoBehaviour
{
    void Awake()
    {
    #if UNITY_IOS
        Application.targetFrameRate = 60;
    #endif

    #if !UNITY_EDITOR
        var count = PlayerPrefs.GetInt("LaunchCount");
        PlayerPrefs.SetInt("LaunchCount", count + 1);
    #endif
    }
}

} // namespace Arpie
