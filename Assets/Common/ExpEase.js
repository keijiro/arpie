#pragma strict

public static class ExpEase {
    public function Out(current : float, target : float, coeff : float) : float {
        return target - (target - current) * Mathf.Exp(coeff * Time.deltaTime); 
    }
    
    public function OutAngle(current : float, target : float, coeff : float) : float {
        return target - Mathf.DeltaAngle(current, target) * Mathf.Exp(coeff * Time.deltaTime); 
    }
    
    public function Out(current : Vector3, target : Vector3, coeff : float) : Vector3 {
        return Vector3.Lerp(target, current, Mathf.Exp(coeff * Time.deltaTime));
    }
    
    public function Out(current : Quaternion, target : Quaternion, coeff : float) : Quaternion {
        if (current == target) {
            return target;
        } else {
            return Quaternion.Lerp(target, current, Mathf.Exp(coeff * Time.deltaTime));
        }
    }
}
