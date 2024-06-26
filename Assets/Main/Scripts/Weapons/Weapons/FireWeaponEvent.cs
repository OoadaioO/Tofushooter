using System;
using UnityEngine;


[DisallowMultipleComponent]
public class FireWeaponEvent : MonoBehaviour
{
    public event Action<FireWeaponEvent, FireWeaponEventArgs> OnFireWeapon;

    public void CallFireWeaponEvent(bool fire, bool firePreviousFrame, 
        AimDirection aimDirection, float aimAngle, 
        float weaponAimAngle, Vector3 weaponAimDirectionVector,
        float fireTime = 0){
        OnFireWeapon?.Invoke(this, new FireWeaponEventArgs(){
            fire = fire,
            firePreviousFrame = firePreviousFrame,
            aimDirection = aimDirection,
            aimAngle = aimAngle,
            weaponAimAngle = weaponAimAngle,
            weaponAimDirectionVector = weaponAimDirectionVector,
            fireTime = fireTime
        });
    }
}

public class FireWeaponEventArgs : EventArgs
{
    public bool fire;
    public bool firePreviousFrame; // Holding fire
    public AimDirection aimDirection;
    public float aimAngle;
    public float weaponAimAngle;
    public Vector3 weaponAimDirectionVector;
    public float fireTime;
}