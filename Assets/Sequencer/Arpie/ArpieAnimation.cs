using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ArpieAnimation : MonoBehaviour
{
    private Quaternion initialRotation;
    private float freq_rx;
    private float freq_ry;
    private float freq_s;
    private bool noScaling;
    public virtual void Start()
    {
        this.initialRotation = this.transform.localRotation;
        this.freq_rx = Random.Range(7f, 13f);
        this.freq_ry = Random.Range(7f, 13f);
        this.freq_s = Random.Range(7f, 13f);
        this.noScaling = QualitySettings.GetQualityLevel() < 2;
    }

    public virtual void Update()
    {
        this.transform.localRotation = (this.initialRotation * Quaternion.AngleAxis(10f * Mathf.Sin(Time.time * this.freq_ry), Vector3.up)) * Quaternion.AngleAxis(10f * Mathf.Sin(Time.time * this.freq_rx), Vector3.right);
        if (!this.noScaling)
        {
            this.transform.localScale = Vector3.one * (1f + (0.1f * Mathf.Sin(this.freq_s * Time.time)));
        }
    }

    public virtual void RemoveArpies()
    {
        this.enabled = false;
    }

    public ArpieAnimation()
    {
        this.freq_rx = 1f;
        this.freq_ry = 1f;
        this.freq_s = 1f;
    }

}