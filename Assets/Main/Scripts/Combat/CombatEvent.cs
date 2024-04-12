using System;
using UnityEngine;

public class CombatEvent : MonoBehaviour {
    public event Action<CombatEvent, DamageEventArg> OnDamageEvent;

    public void CallOnDamageEvent(Vector2 damageDirection, int damangeAmount, float knockbackThrust) {
        OnDamageEvent?.Invoke(this, new DamageEventArg() {
            damageDirection = damageDirection,
            damageAmount = damangeAmount,
            knockBackThrust = knockbackThrust
        });
    }

}


public class DamageEventArg : EventArgs {
    public Vector2 damageDirection;
    public int damageAmount;
    public float knockBackThrust;
}