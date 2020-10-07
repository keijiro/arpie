using UnityEngine;

namespace Arpie {

class ParticleSystemAutoDestructor : MonoBehaviour
{
    ParticleSystem _particleSystem;

    void Start()
      => _particleSystem = GetComponent<ParticleSystem>();

    void LateUpdate()
    {
        if (!_particleSystem.IsAlive()) Destroy(gameObject);
    }
}

} // namespace Arpie
