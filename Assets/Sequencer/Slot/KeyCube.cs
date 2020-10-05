using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class KeyCube : MonoBehaviour
{
    private float vibe;
    private float spin;
    private Transform mesh;
    private static Color[] baseColors;
     // Pentatonic
     // Diatonic
     // Pentatonic + IIV
     // Major blues
     // Ryukyu
    public virtual void Awake()
    {
        this.mesh = this.transform.Find("Mesh");
    }

    public virtual void SetColor(int scaleIndex, int degree)
    {
        float brightness = (degree & 1) != 0 ? 0.7f : 0.9f;
        this.mesh.GetComponent<Renderer>().material.color = KeyCube.baseColors[scaleIndex % KeyCube.baseColors.Length] * brightness;
    }

    public virtual void Update()
    {
        this.mesh.localRotation = Quaternion.AngleAxis(this.spin * 360f, Vector3.up);
        this.mesh.localScale = (Vector3.one * (1f + ((0.2f * this.vibe) * Mathf.Sin(40f * Time.time)))) * (1f - (0.5f * this.spin));
        this.vibe = ExpEase.Out(this.vibe, 0f, -8f);
        this.spin = ExpEase.Out(this.spin, 0f, -8f);
    }

    public virtual void KeyOn()
    {
        this.vibe = 1f;
    }

    public virtual void RemoveArpies()
    {
        this.vibe = 1f;
        this.spin = 1f;
    }

    static KeyCube()
    {
        KeyCube.baseColors = new Color[] {new Color(1f, 1f, 1f), new Color(0.7f, 1f, 0.7f), new Color(0.7f, 1f, 1f), new Color(0.7f, 0.7f, 1f), new Color(1f, 1f, 0.7f)};
    }

}
