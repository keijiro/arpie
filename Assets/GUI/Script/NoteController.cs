using UnityEngine;

namespace Arpie {

class NoteController : MonoBehaviour
{
    enum NoteType { Spawn, Cube }

    [SerializeField] NoteType _noteType = NoteType.Spawn;

    float _fade, _param;

    Material _material;

    void SetAlpha(float alpha)
    {
        if (_material == null) _material = GetComponent<Renderer>().material;
        _material.color = new Color(1, 1, 1, alpha * 0.9f);
    }

    System.Collections.IEnumerator Start()
    {
        if (PlayerPrefs.GetInt("LaunchCount") > 2)
        {
            Destroy(gameObject);
            yield break;
        }

        SetAlpha(0);

        if (_noteType == NoteType.Spawn)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else // _noteType == NoteType.Cube
        {
            while (TouchInput.SpawnCount == 0) yield return null;
            yield return new WaitForSeconds(2);
        }

        _fade = 2;

        while (true)
        {
            if (_noteType == NoteType.Spawn)
            {
                if (TouchInput.SpawnCount > 0) break;
            }
            else // _noteType == NoteType.Cube
            {
                if (TouchInput.CubeCount > 0) break;
            }
            yield return null;
        }

        _fade = -2;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    void Update()
    {
        _param = Mathf.Clamp01(_param + _fade * Time.deltaTime);
        SetAlpha(_param);

        var t = Time.time + 10;
        var dx = 0.05f * Mathf.Sin(t * 4.97f);
        var dy = 0.05f * Mathf.Sin(t * 5.33f);
        transform.localPosition = new Vector3(dx, dy, 0);
    }
}

} // namespace Arpie
