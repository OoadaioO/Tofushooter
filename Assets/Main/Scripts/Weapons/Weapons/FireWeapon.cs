using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(WeaponFiredEvent))]
[DisallowMultipleComponent]
public class FireWeapon : MonoBehaviour {

    private float firePreChargeTimer, fireRateCoolDownTimer;

    private FireWeaponEvent fireWeaponEvent;
    private WeaponFiredEvent weaponFiredEvent;
    private ActivateWeapon activeWeapon;



    private void Awake() {

        activeWeapon = GetComponent<ActivateWeapon>();
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        weaponFiredEvent = GetComponent<WeaponFiredEvent>();
    }

    private void OnEnable() {
        fireWeaponEvent.OnFireWeapon += FireWeaponEvent_OnFireWeapon;
    }
    private void OnDisable() {
        fireWeaponEvent.OnFireWeapon -= FireWeaponEvent_OnFireWeapon;
    }

    private void Update() {
        fireRateCoolDownTimer -= Time.deltaTime;
    }


    private void FireWeaponEvent_OnFireWeapon(FireWeaponEvent fireWeaponEvent, FireWeaponEventArgs fireWeaponEventArgs) {
        WeaponFire(fireWeaponEventArgs);
    }

    private void WeaponFire(FireWeaponEventArgs fireWeaponEventArgs) {

        WeaponPreCharge(fireWeaponEventArgs);


        if (fireWeaponEventArgs.fire) {
            if (IsWeaponReadyToFire()) {
                FireAmmo(fireWeaponEventArgs.aimAngle, fireWeaponEventArgs.weaponAimAngle,
                    fireWeaponEventArgs.weaponAimDirectionVector, fireWeaponEventArgs.fireTime);

                ResetCoolDownTimer();
                ResetPrechargeTimer();
            }
        }
    }

    private void WeaponPreCharge(FireWeaponEventArgs fireWeaponEventArgs) {
        if (fireWeaponEventArgs.firePreviousFrame) {
            firePreChargeTimer -= Time.deltaTime;
        } else {
            ResetPrechargeTimer();
        }
    }

    private bool IsWeaponReadyToFire() {

        // 蓄力时间 || cd时间 未触达
        if (firePreChargeTimer > 0f || fireRateCoolDownTimer > 0f) {
            return false;
        }

        return true;
    }


    private void FireAmmo(float aimAngle, float weaponAimAngle, Vector3 weaponAimDirectionVector, float fireTime) {

        AmmoDetailsSO ammoDetailsSO = activeWeapon.ammoDetailsSO;
        if (ammoDetailsSO != null) {
            StartCoroutine(FireAmmoRoutine(ammoDetailsSO, aimAngle, weaponAimAngle, weaponAimDirectionVector));

        }
    }

    private IEnumerator FireAmmoRoutine(AmmoDetailsSO currentAmmo, float aimAngle, float weaponAimAngle, Vector3 weaponAimDirectionVector) {

        int ammoCounter = 0;
        int ammoPerShot = UnityEngine.Random.Range(currentAmmo.ammoSpawnAmountMin, currentAmmo.ammoSpawnAmountMax + 1);

        float ammoSpawnInterval;
        if (ammoPerShot > 1) {
            ammoSpawnInterval = UnityEngine.Random.Range(currentAmmo.ammoSpawnIntervalMin, currentAmmo.ammoSpawnIntervalMax);
        } else {
            ammoSpawnInterval = 0f;
        }


        while (ammoCounter < ammoPerShot) {
            ammoCounter++;


            float ammoSpeed = UnityEngine.Random.Range(currentAmmo.ammoSpeedMin, currentAmmo.ammoSpeedMax);

            GameObject ammoPrefab = currentAmmo.ammoPrefabArray[UnityEngine.Random.Range(0, currentAmmo.ammoPrefabArray.Length)];

            Ammo ammo = PoolManager.Instance.ReuseComponent<Ammo>(ammoPrefab, activeWeapon.GetShootPosition(), Quaternion.identity);


            ammo.InitialiseAmmo(currentAmmo, aimAngle, weaponAimAngle, ammoSpeed, weaponAimDirectionVector);

            yield return new WaitForSeconds(ammoSpawnInterval);
        }


        weaponFiredEvent.CallWeaponFiredEvent();

        WeaponShootEffect(aimAngle);

        WeaponSoundEffect();
    }

    private void ResetCoolDownTimer() {
        fireRateCoolDownTimer = activeWeapon.weaponFireRate;
    }

    private void WeaponShootEffect(float aimAngle) {
        // todo
    }

    private void ResetPrechargeTimer() {
        firePreChargeTimer = activeWeapon.weaponPrechargeTime;
    }

    private void WeaponSoundEffect() {
        // todo
    }
}
