using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {


    // 子弹速度
    private float ammoSpeed;
    // 最大射程
    private float ammoRange;
    // 避免同时碰撞2个对象
    private bool isColliding = false;
    // 子弹角度
    private float fireDirectionAngle;
    // 子弹方向
    private Vector3 fireDirectionVector;
    
    private AmmoDetailsSO ammoDetails;


    private void Update() {

        Vector3 distanceVector = ammoSpeed * Time.deltaTime * fireDirectionVector;
        transform.position += distanceVector;

        ammoRange -= distanceVector.magnitude;

        if (ammoRange < 0f) {
            DisableAmmo();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // 防止二次触发
        if (isColliding) return;
        DealDamage(other);
    }


    private void DealDamage(Collider2D collision) {

        // TODO 
        // if (collision.TryGetComponent<Health>(out var health)) {

        //     isColliding = true;
        //     health.TakeDamage(ammoDetails.ammoDamage);
        // }

    }



    public void InitialiseAmmo(AmmoDetailsSO ammoDetails, float aimAngle, float weaponAimAngle, float ammoSpeed, Vector3 weaponAimDirectionVector) {
        this.ammoDetails = ammoDetails;
        isColliding = false;
        SetFireDirection(aimAngle, weaponAimAngle, weaponAimDirectionVector);
        this.ammoSpeed = ammoSpeed;
        this.ammoRange = 10;
        gameObject.SetActive(true);

    }

    private void SetFireDirection(float aimAngle, float weaponAimAngle, Vector3 weaponAimDirectionVector) {

        float randomSpread = Random.Range(ammoDetails.ammoSpreadMin, ammoDetails.ammoSpreadMax);
        int spreadToggle = Random.Range(0, 2) * 2 - 1;

        if (weaponAimDirectionVector.magnitude < Settings.useAimAngleDistance) {
            fireDirectionAngle = aimAngle;
        } else {
            fireDirectionAngle = weaponAimAngle;
        }

        fireDirectionAngle += spreadToggle * randomSpread;

        transform.rotation = Quaternion.Euler(0, 0, fireDirectionAngle);

        fireDirectionVector = HelperUtilities.GetDirectionVectorFromAngle(fireDirectionAngle);
    }

    private void DisableAmmo() {
        gameObject.SetActive(false);
    }
}
