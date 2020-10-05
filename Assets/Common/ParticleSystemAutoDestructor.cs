using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ParticleSystemAutoDestructor : MonoBehaviour
{
    public virtual void LateUpdate()
    {
        if (!this.GetComponent<ParticleSystem>().IsAlive())
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

}