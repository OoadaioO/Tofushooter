using System;
using UnityEngine;

[DisallowMultipleComponent]
public class AimWeaponEvent : MonoBehaviour {
    public event Action<AimWeaponEvent, AimWeaponEventArgs> OnWeaponAim;

    public void CallAimWeaponEvent(AimDirection aimDirection, float aimAngle, AimDirection weaponAimDirection,float weaponAimAngle, Vector3 weaponAimDirectionVector,bool useWeaponDirection) {
        OnWeaponAim?.Invoke(this, new AimWeaponEventArgs() {
            aimDirection = aimDirection,
            aimAngle = aimAngle,
            weaponAimAngle = weaponAimAngle,
            weaponAimDirectionVector = weaponAimDirectionVector,
            weaponAimDirection = weaponAimDirection,
            useWeaponDirection = useWeaponDirection,
        });
    }

}

public class AimWeaponEventArgs : EventArgs {
    public AimDirection aimDirection;
    public float aimAngle;

    public float weaponAimAngle;
    public Vector3 weaponAimDirectionVector;
    public AimDirection weaponAimDirection;

    public bool useWeaponDirection;

}