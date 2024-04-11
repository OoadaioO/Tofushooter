using UnityEngine;

[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class Player : MonoBehaviour {
    [HideInInspector] public FireWeaponEvent fireWeaponEvent;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;


    public float hitImmunityTime = 0.2f;
    public bool isImmuneAfterHit = true;

    private void Awake() {
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        idleEvent = GetComponent<IdleEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Vector3 GetPlayerPosition() {
        return transform.position;
    }

}
