using System;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(MovementToPosition))]
[RequireComponent(typeof(AnimateEnemy))]
[RequireComponent(typeof(EnemyMovmentAI))]
[RequireComponent(typeof(AimWeaponEvent))]

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthEvent))]
[RequireComponent(typeof(Destroyed))]
[RequireComponent(typeof(DestroyedEvent))]

[DisallowMultipleComponent]
public class Enemy : MonoBehaviour {

    [HideInInspector] public FireWeaponEvent fireWeaponEvent;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;


    [SerializeField] private int enemyHealthAmount = 5;
    [SerializeField] private float moveSpeedMin;
    [SerializeField] private float moveSpeedMax;



    private Health health;
    private HealthEvent healthEvent;
    private EnemyMovmentAI enemyMovmentAI;

    public float hitImmunityTime = 0.2f;
    public bool isImmuneAfterHit = true;

    private void Awake() {
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        idleEvent = GetComponent<IdleEvent>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        health = GetComponent<Health>();
        healthEvent = GetComponent<HealthEvent>();
        enemyMovmentAI = GetComponent<EnemyMovmentAI>();

    }



    private void OnEnable() {
        healthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
    }
    private void OnDisable() {
        healthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
    }
    private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs) {
        if (healthEventArgs.healthAmount <= 0) {
            EnemyDestoryed();
        }
    }

    private void EnemyDestoryed() {
        DestroyedEvent destroyedEvent = GetComponent<DestroyedEvent>();
        destroyedEvent.CallDestroyedEvent(false, health.GetStartingHealth());
    }


    private void SetStartingHealth(int enemyHealthAmount) {
        health.SetStartingHealth(enemyHealthAmount);
    }

    public void EnemyInitialization(int enemySpawnNumber) {
        SetEnemyMovmentUpdateFrame(enemySpawnNumber);
        SetEnemyStartingHealth();
    }

    private void SetEnemyMovmentUpdateFrame(int enemySpawnNumber) {
        enemyMovmentAI.SetUpdateFrameNumber(enemySpawnNumber % Settings.targetFrameRateToSpreadPathfindingOver);
        enemyMovmentAI.moveSpeed = UnityEngine.Random.Range(moveSpeedMin,moveSpeedMax);
    }

    private void SetEnemyStartingHealth() {
        SetStartingHealth(enemyHealthAmount);
    }
}
