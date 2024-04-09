using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeapon : MonoBehaviour {

    public AmmoDetailsSO ammoDetailsSO;

    [SerializeField] private Transform weaponShootPositionTransform;

    [SerializeField] private Transform weaponEffectPositionTransform;

    [Space(10)]
    [Header("weapon")]
    public float weaponFireRate = .5f; // weaponFireRate需要 > ammoSpawnIntervalMax*ammoSpawnAmountMax
    public float weaponPrechargeTime = 0f;

    public Vector3 GetShootPosition() {
        return weaponShootPositionTransform.position;
    }
    public Vector3 GetShootEffectPosition() {
        return weaponEffectPositionTransform.position;
    }

}
