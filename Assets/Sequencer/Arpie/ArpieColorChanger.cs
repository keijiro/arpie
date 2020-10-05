using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ArpieColorChanger : MonoBehaviour
{
    public Color[] colors;
    public static Material[] materials;
    public static int counter;
    public virtual void Awake()
    {
        if (ArpieColorChanger.materials == null)
        {
            ArpieColorChanger.materials = new Material[this.colors.Length];
            int i = 0;
            while (i < this.colors.Length)
            {
                ArpieColorChanger.materials[i] = new Material(this.GetComponent<Renderer>().material);
                ArpieColorChanger.materials[i].color = this.colors[i];
                i++;
            }
        }
        this.GetComponent<Renderer>().material = ArpieColorChanger.materials[ArpieColorChanger.counter];
        if (++ArpieColorChanger.counter == ArpieColorChanger.materials.Length)
        {
            ArpieColorChanger.counter = 0;
        }
    }

}