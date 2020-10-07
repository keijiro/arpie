using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Arpie {

class AppInitializer : MonoBehaviour
{
    const string LaunchCountKey = "launch count";

    IEnumerator Start()
    {
        yield return null;

        // PlayerPrefs.DeleteKey(LaunchCountKey);

        var count = PlayerPrefs.GetInt(LaunchCountKey);
        PlayerPrefs.SetInt(LaunchCountKey, count + 1);

        yield return null;
        yield return null;

        SceneManager.LoadScene(1);
    }
}

} // namespace Arpie
