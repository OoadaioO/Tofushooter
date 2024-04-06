using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
public class Player : MonoBehaviour {
    public MovementByVelocityEvent MovementByVelocityEvent { get; set; }
    public IdleEvent IdleEvent { get; set; }

    private void Awake() {
        MovementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        IdleEvent = GetComponent<IdleEvent>();
    }
}
