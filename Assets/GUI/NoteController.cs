using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class NoteController : MonoBehaviour
{
    public int noteType;
    public float baseAlpha;
    private string state;
    private float param;
    private float sx;
    private float sy;
    public virtual void Awake()
    {
        if (PlayerPrefs.GetInt("launch count") > 2)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public virtual IEnumerator Start()
    {
        this.sx = this.sx * Random.Range(0.9f, 1f);
        this.sy = this.sy * Random.Range(0.9f, 1f);

        {
            float _10 = 0f;
            Color _11 = this.GetComponent<Renderer>().material.color;
            _11.a = _10;
            this.GetComponent<Renderer>().material.color = _11;
        }
        if (this.noteType == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            while (TouchInput.spawnCount == 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(2f);
        }
        this.state = "fade in";
        while (true)
        {
            if (this.noteType == 0)
            {
                if (TouchInput.spawnCount > 0)
                {
                    break;
                }
            }
            else
            {
                if (TouchInput.cubeCount > 0)
                {
                    break;
                }
            }
            yield return null;
        }
        this.state = "fade out";
        yield return new WaitForSeconds(0.5f);
        UnityEngine.Object.Destroy(this.gameObject);
    }

    public virtual void Update()
    {
        if (this.state == "fade in")
        {
            this.param = Mathf.Min(this.param + (Time.deltaTime * 2), 1f);
        }
        else
        {
            if (this.state == "fade out")
            {
                this.param = Mathf.Max(this.param - (Time.deltaTime * 2), 0f);
            }
        }
        float dx = 0.05f * Mathf.Sin(Time.time * this.sx);
        float dy = 0.05f * Mathf.Sin(Time.time * this.sy);
        this.transform.localPosition = new Vector3(dx, dy, 0f);

        {
            float _12 = this.baseAlpha * this.param;
            Color _13 = this.GetComponent<Renderer>().material.color;
            _13.a = _12;
            this.GetComponent<Renderer>().material.color = _13;
        }
    }

    public NoteController()
    {
        this.baseAlpha = 0.75f;
        this.state = "";
        this.sx = 6f;
        this.sy = 6f;
    }

}
