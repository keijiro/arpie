using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class AppInitializer : MonoBehaviour
{
    public virtual IEnumerator Start()
    {
        yield return null;
        //	PlayerPrefs.DeleteKey("launch count");
        PlayerPrefs.SetInt("launch count", PlayerPrefs.GetInt("launch count") + 1);
        yield return null;
        yield return null;
        Application.LoadLevel(1);
    }

}