using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ResetButton : MonoBehaviour
{
    private static int resetCount;
    private float initialScale;
    private float vibe;
    public virtual void Start()
    {
        this.initialScale = this.transform.localScale.x;
    }

    public virtual void Update()
    {
        float param = this.vibe * Mathf.Sin(Time.time * 30f);
        this.transform.localScale = Vector3.one * (this.initialScale * (1f + (0.5f * param)));
        this.vibe = ExpEase.Out(this.vibe, 0f, -8f);
    }

    public virtual IEnumerator DoReset()
    {
        this.vibe = 1f;
        ResetButton.resetCount++;
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        int i = 0;
        while (i < slots.Length)
        {
            GameObject slot = slots[i];
            slot.BroadcastMessage("RemoveArpies");
            slot.GetComponentInChildren<KeyCube>().SetColor(ResetButton.resetCount, i);
            slot.GetComponentInChildren<KeyAudio>().SetKey(ResetButton.resetCount, i);
            yield return new WaitForSeconds(0.03f);
            i++;
        }
    }

    public ResetButton()
    {
        this.initialScale = 1f;
    }

}