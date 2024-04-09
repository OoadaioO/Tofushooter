using UnityEngine;
using System;

[DisallowMultipleComponent]
public class WeaponFiredEvent : MonoBehaviour
{
    public Action<WeaponFiredEvent, WeaponFiredEventArgs> OnWeaponFired;
    public void CallWeaponFiredEvent(){
        OnWeaponFired?.Invoke(this, new WeaponFiredEventArgs() {});
    }

}


public class WeaponFiredEventArgs : EventArgs
{
}