using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ArpieMovement : MonoBehaviour
{
    public float stepPerMin;
    public float interval;
    public float quantization;
    public GameObject killFxPrefab;
    private float phase;
    private int step;
    private bool delaying;
    public virtual IEnumerator Start()
    {
        // Quantize the current time.
        float waitFor = (60f * (Mathf.FloorToInt((this.stepPerMin * Time.time) / 60f) + 1)) / this.stepPerMin;
        // Apply the quantization ratio.
        waitFor = Mathf.Lerp(Time.time, waitFor, this.quantization);
        // Wait for the timing.
        while (Time.time < waitFor)
        {
            yield return null;
        }
        // Start!
        this.delaying = false;
    }

    public virtual void Update()
    {
        if (this.delaying)
        {
            return;
        }
        this.phase = this.phase + ((this.stepPerMin * Time.deltaTime) / (60f * this.interval));

        {
            float _14 = this.interval * Mathf.Abs(Mathf.Cos(this.phase * Mathf.PI));
            Vector3 _15 = this.transform.localPosition;
            _15.y = _14;
            this.transform.localPosition = _15;
        }
        int stepNew = Mathf.FloorToInt(this.phase + 0.5f);
        if (stepNew > this.step)
        {
            this.transform.parent.gameObject.BroadcastMessage("KeyOn");
            this.step = stepNew;
        }
    }

    public virtual void RemoveArpies()
    {
        this.enabled = false;
    }

    public ArpieMovement()
    {
        this.stepPerMin = 200f;
        this.interval = 1f;
        this.quantization = 0.8f;
        this.delaying = true;
    }

}