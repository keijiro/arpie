using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class KeyboardBuilder : MonoBehaviour
{
    public int numberOfKeys;
    public float panning;
    public float volume;
    public float highDecay;
    public GameObject slotPrefab;
    public virtual void Start()
    {
        int i = 0;
        while (i < this.numberOfKeys)
        {
            Vector3 position = this.transform.position + (Vector3.right * i);
            GameObject slot = UnityEngine.Object.Instantiate(this.slotPrefab, position, Quaternion.identity) as GameObject;
            slot.GetComponentInChildren<KeyCube>().SetColor(0, i);
            slot.GetComponentInChildren<Arpie.KeyAudio>().SetKey(0, i);
            AudioSource source = slot.GetComponentInChildren<AudioSource>();
            source.panStereo = this.panning * ((i / this.numberOfKeys) - 0.5f);
            source.volume = this.volume - ((this.highDecay * i) / this.numberOfKeys);
            i++;
        }
        UnityEngine.Object.Destroy(this.gameObject);
    }

    public KeyboardBuilder()
    {
        this.numberOfKeys = 16;
        this.panning = 1.4f;
        this.volume = 0.35f;
        this.highDecay = 0.08f;
    }

}
