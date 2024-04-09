using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoDetails_", menuName = "ScriptableObjects/Weapons/Ammo Details")]
public class AmmoDetailsSO : ScriptableObject {

    [Space(10)]
    [Header("Basic Ammo Details")]
    public string ammoName;
    

    [Space(10)]
    [Header("Ammo Sprite,Prefab & Materials")]
    public GameObject[] ammoPrefabArray;


    [Space(10)]
    [Header("Ammo base parameters")]
    public int ammoDamage = 1;
    public float ammoSpeedMin = 15f;
    public float ammoSpeedMax = 15f;
    public float ammoRange = 20f;
    public float ammoRotationSpeed = 1f;


    [Space(10)]
    [Header("Ammo spread details")]
    public float ammoSpreadMin = 0f;
    public float ammoSpreadMax = 0f;


    [Space(10)]
    [Header("Ammo spawn details")]

    public int ammoSpawnAmountMin = 1;
    public int ammoSpawnAmountMax = 1;
    public float ammoSpawnIntervalMin = 0f;
    public float ammoSpawnIntervalMax = 0f;




    #region Validation
#if UNITY_EDITOR
    private void OnValidate() {

        HelperUtilities.ValidateCheckEmptyString(this, nameof(ammoName), ammoName);

        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(ammoPrefabArray), ammoPrefabArray);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoDamage), ammoDamage, false);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(ammoSpeedMin), ammoSpeedMin, nameof(ammoSpeedMax), ammoSpeedMax, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(ammoRange), ammoRange, false);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(ammoSpreadMin), ammoSpreadMin, nameof(ammoSpreadMax), ammoSpreadMax, true);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(ammoSpawnAmountMin), ammoSpawnAmountMin, nameof(ammoSpawnAmountMax), ammoSpawnAmountMax, false);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(ammoSpawnIntervalMin), ammoSpawnIntervalMin, nameof(ammoSpawnIntervalMax), ammoSpawnIntervalMax, true);


    }
#endif
    #endregion

}
