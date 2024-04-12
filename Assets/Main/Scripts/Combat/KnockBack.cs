using System.Collections;
using UnityEditor;
using UnityEngine;

[DisallowMultipleComponent]
public class KnockBack : MonoBehaviour {
    [HideInInspector] public bool isKnockBack;

    [SerializeField] private float knockbackTimeDuration = .1f;

    private Rigidbody2D rigidBody2D;


    private void Awake() {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Knockback(Vector3 forceDirection, float knockbackThrust) {
        ApplyKnock(forceDirection, knockbackThrust);
    }

    private void ApplyKnock(Vector3 forceDirection, float knockbackThrust) {
        isKnockBack = true;

        StartCoroutine(KnockRoutine(forceDirection, knockbackThrust));

    }

    private IEnumerator KnockRoutine(Vector3 forceDirection, float knockbackThrust) {

        Vector2 difference = rigidBody2D.mass * knockbackThrust * forceDirection.normalized;
        rigidBody2D.AddForce(difference, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTimeDuration);
        isKnockBack = false;
    }

}
