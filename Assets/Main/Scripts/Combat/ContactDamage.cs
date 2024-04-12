using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour {
    [SerializeField] private int contactDamageAmount;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float knockbackThrust = 2f;

    private bool isCollding = false;


    private void OnCollisionStay2D(Collision2D other) {

        DealContactDamage(other.gameObject.layer, other.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D other) {

        DealContactDamage(other.gameObject.layer, other.transform.position);
    }



    private void OnTriggerEnter2D(Collider2D other) {

        DealContactDamage(other.gameObject.layer, other.transform.position);
    }

    private void OnTriggerStay2D(Collider2D other) {

        DealContactDamage(other.gameObject.layer, other.transform.position);
    }


    private void DealContactDamage(int layer, Vector3 collidePosition) {
        int collisionObjectLayerMask = 1 << layer;
        if ((layerMask & collisionObjectLayerMask) == 0) {
            return;
        }


        if (isCollding) return;


        if (TryGetComponent<Health>(out var health)) {

            StartCoroutine(ResetContactDamageRoutine(Settings.contactDamageCollisionResetDelay));

            isCollding = true;
            health.TakeDamage(collidePosition, knockbackThrust, contactDamageAmount);
        }

    }

    private IEnumerator ResetContactDamageRoutine(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        isCollding = false;
    }





}
