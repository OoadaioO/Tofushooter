using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerControl))]


[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(Destroyed))]
[RequireComponent(typeof(DestroyedEvent))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthEvent))]
[RequireComponent(typeof(KnockBack))]




[DisallowMultipleComponent]
public class Player : MonoBehaviour {

    [HideInInspector] public FireWeaponEvent fireWeaponEvent;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Health health;
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public PlayerControl playerControl;


    [SerializeField] private int playerHealthAmount;


    public float hitImmunityTime = 0.2f;
    public bool isImmuneAfterHit = true;
    public float shootDistance = 10f;

    private void Awake() {
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        idleEvent = GetComponent<IdleEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerControl = GetComponent<PlayerControl>();

        health = GetComponent<Health>();
        healthEvent = GetComponent<HealthEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();

    }



    private void OnEnable() {
        healthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
    }
    private void OnDisable() {
        healthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
    }

    private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs) {

        if (healthEventArgs.healthAmount <= 0) {
            destroyedEvent.CallDestroyedEvent(true, 0);
        }
    }

    public void Initilaize() {
        SetPlayerHealth();
    }

    private void SetPlayerHealth() {
        health.SetStartingHealth(playerHealthAmount);
    }


    public Vector3 GetPlayerPosition() {
        return transform.position;
    }




#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,shootDistance);
    }

#endif
}
