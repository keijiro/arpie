using UnityEngine;

namespace Arpie {

class TouchInput : MonoBehaviour
{
    [SerializeField] GameObject _arpiePrefab = null;

    public static int SpawnCount { get; private set; }
    public static int CubeCount { get; private set; }

    void Update()
    {
    #if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
        foreach (var touch in Input.touches)
            if (touch.phase == TouchPhase.Began)
                DoTouch(touch.position);
    #else
        if (Input.GetMouseButtonDown(0))
            DoTouch(Input.mousePosition);
    #endif
    }

    void DoTouch(Vector3 screenCoord)
    {
        var hit = default(RaycastHit);
        var ray = Camera.main.ScreenPointToRay(screenCoord);
        if (!Physics.Raycast(ray, out hit)) return;

        if (hit.collider.name == "Key Cube")
        {
            hit.transform.parent.BroadcastMessage("RemoveArpies");
            CubeCount++;
        }
        else
        {
            if (hit.collider.name == "Reset Button")
            {
                hit.transform.SendMessage("DoReset");
            }
            else
            {
                SpawnWithHit(hit);
                SpawnCount++;
            }
        }
    }

    void SpawnWithHit(RaycastHit hit)
    {
        var interval = int.Parse(hit.collider.name);
        var go = Instantiate(_arpiePrefab, hit.transform.parent.parent);
        go.transform.localPosition = new Vector3(0, interval, 0);
        go.GetComponent<ArpieMovement>().Interval = interval;
    }
}

} // namespace Arpie
