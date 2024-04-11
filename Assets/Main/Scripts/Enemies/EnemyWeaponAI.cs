using UnityEngine;


[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]
public class EnemyWeaponAI : MonoBehaviour {

    [Space(10)]
    [Header("Base")]
    [SerializeField] private Transform weaponShootPosition;
    [SerializeField] private float aimIntervalTimer = 0.3f;

    [Space(10)]
    [Header("Enemy Weapon Settings")]
    [SerializeField] private float firingDurationMin;
    [SerializeField] private float firingDurationMax;

    [SerializeField] private float firingIntervalMin;
    [SerializeField] private float firingIntervalMax;

    [Space(10)]
    [Header("Ammo settings")]
    [SerializeField] private AmmoDetailsSO ammoDetails;


    private Enemy enemy;
    private float firingIntervalTimer;
    private float firingDurationTimer;




    private Vector3 playerDirectionVector;
    private float enemyAngleDegrees;
    private AimDirection enemyAimDirection;

    private void Awake() {
        enemy = GetComponent<Enemy>();
    }

    private void Start() {

        firingIntervalTimer = WeaponShootInterval();
        firingDurationTimer = WeaponShootDuration();
    }


    private void Update() {
        firingIntervalTimer -= Time.deltaTime;
        aimIntervalTimer -= Time.deltaTime;

        if (aimIntervalTimer < 0f) {
            AimWeapon();
        }


        if (firingIntervalTimer < 0f) {
            if (firingDurationTimer >= 0f) {
                firingDurationTimer -= Time.deltaTime;
                FireWeapon();
            } else {
                firingIntervalTimer = WeaponShootInterval();
                firingDurationTimer = WeaponShootDuration();
            }
        }
    }

    private void AimWeapon() {
        // 玩家在敌人空间向量
        playerDirectionVector = GameManager.Instance.GetPlayer().GetPlayerPosition() - transform.position;

        // 武器相对于玩家的射击角度
        enemyAngleDegrees = HelperUtilities.GetAngleFromVector(playerDirectionVector);

        enemyAimDirection = HelperUtilities.GetAimDirection8(enemyAngleDegrees);

        // 触发武器瞄准事件
        enemy.aimWeaponEvent.CallAimWeaponEvent(enemyAimDirection, enemyAngleDegrees, enemyAngleDegrees, playerDirectionVector);
    }

    private float WeaponShootDuration() {
        return Random.Range(firingDurationMin, firingDurationMax);
    }
    private float WeaponShootInterval() {
        return Random.Range(firingIntervalMin, firingIntervalMax);
    }


    private void FireWeapon() {


        // 敌人有武器的情况下触发
        if (ammoDetails != null) {

            float enemyAmmoRange = ammoDetails.ammoRange;

            // 在射程范围
            if (playerDirectionVector.magnitude <= enemyAmmoRange) {

                // 确认需要视野，并且玩家在视野内

                enemy.fireWeaponEvent.CallFireWeaponEvent(true, true, enemyAimDirection, enemyAngleDegrees, enemyAngleDegrees, playerDirectionVector);
            }
        }

    }


}
