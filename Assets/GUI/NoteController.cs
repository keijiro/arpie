using UnityEngine;

namespace Arpie {

class NoteController : MonoBehaviour
{
    enum NoteType { Spawn, Cube }
    enum State { FadeOut, FadeIn }

    [SerializeField] NoteType _noteType = NoteType.Spawn;
    [SerializeField] float _baseAlpha = 0;

    State _state;
    float _sx, _sy, _param;

    Material _material;

    void SetAlpha(float alpha)
    {
        if (_material == null)
            _material = GetComponent<Renderer>().material;

        var color = _material.color;
        color.a = alpha;
        _material.color = color;
    }

    System.Collections.IEnumerator Start()
    {
        if (PlayerPrefs.GetInt("LaunchCount") > 2)
        {
            Destroy(gameObject);
            yield break;
        }

        _sx = Random.Range(5.4f, 6);
        _sy = Random.Range(5.4f, 6);

        SetAlpha(0);

        if (_noteType == NoteType.Spawn)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else // _noteType == NoteType.Cube
        {
            while (TouchInput.SpawnCount == 0)
                yield return null;
            yield return new WaitForSeconds(2);
        }

        _state = State.FadeIn;

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

        _state = State.FadeOut;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    void Update()
    {
        var t = Time.time + 10;
        var dt = Time.deltaTime;

        if (_state == State.FadeIn)
            _param = Mathf.Min(_param + dt * 2, 1);
        else // _state == State.FadeOut
            _param = Mathf.Max(_param - dt * 2, 0);

        SetAlpha(_baseAlpha * _param);

        var dx = 0.05f * Mathf.Sin(t * _sx);
        var dy = 0.05f * Mathf.Sin(t * _sy);

        transform.localPosition = new Vector3(dx, dy, 0);
    }
}

} // namespace Arpie
