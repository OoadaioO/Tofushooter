using UnityEngine;

[RequireComponent(typeof(AnimatePlayer))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    [HideInInspector] public Animator animator;

    private void Awake() {
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        idleEvent = GetComponent<IdleEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        animator = GetComponent<Animator>();
    }
}
