using UnityEngine;

namespace Arpie {

static class ExpEase
{
    public static float Out(float current, float target, float coeff)
      => target - ((target - current) * Mathf.Exp(coeff * Time.deltaTime));
}

} // namespace Arpie
