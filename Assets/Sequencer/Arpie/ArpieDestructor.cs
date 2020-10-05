using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ArpieDestructor : MonoBehaviour
{
    public GameObject explosion;
    private Vector3 initPosition;
    private float time;
    public virtual void Update()
    {
        this.time = this.time - (Time.deltaTime * 5f);
        if (this.time > 0f)
        {
            this.transform.position = this.initPosition + (((((Random.onUnitSphere * this.time) * this.time) * this.time) * this.time) * 0.4f);
            this.transform.localScale = Vector3.one * (((1f - Mathf.Abs(Mathf.Cos((0.75f * Mathf.PI) * this.time))) * 1.2f) + 0.5f);
        }
        else
        {
            if (this.time < -0.2f)
            {
                UnityEngine.Object.Instantiate(this.explosion, this.transform.position, this.transform.rotation);
                UnityEngine.Object.Destroy(this.gameObject);
            }
        }
    }

    public virtual void RemoveArpies()
    {
        this.initPosition = this.transform.position;
        this.enabled = true;
    }

    public ArpieDestructor()
    {
        this.time = 1f;
    }

}