using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class TouchInput : MonoBehaviour
{
    public static int spawnCount;
    public static int cubeCount;
    public GameObject arpiePrefab;
    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.DoTouch(Input.mousePosition);
        }
    }

    private void DoTouch(Vector3 screenCoord)
    {
        RaycastHit hit = default(RaycastHit);
        Ray ray = Camera.main.ScreenPointToRay(screenCoord);
        if (Physics.Raycast(ray, out hit) && hit.collider)
        {
            if (hit.collider.name == "Key Cube")
            {
                hit.transform.parent.BroadcastMessage("RemoveArpies");
                TouchInput.cubeCount++;
            }
            else
            {
                if (hit.collider.name == "Reset Button")
                {
                    hit.transform.SendMessage("DoReset");
                }
                else
                {
                    this.SpawnWithHit(hit);
                    TouchInput.spawnCount++;
                }
            }
        }
    }

    private void SpawnWithHit(RaycastHit hit)
    {
        int interval = int.Parse(hit.collider.name);
        GameObject go = UnityEngine.Object.Instantiate(this.arpiePrefab);
        go.transform.parent = hit.transform.parent.parent;
        go.transform.localPosition = new Vector3(0, interval, 0);
        go.GetComponent<ArpieMovement>().interval = interval;
    }

}