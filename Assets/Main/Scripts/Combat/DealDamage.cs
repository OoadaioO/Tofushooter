using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CombatEvent))]
public class DealDamage : MonoBehaviour {

    [SerializeField] public float damageImmunityTimeMax = 0f;

    private CombatEvent combatEvent;

    private bool isDamageImmunity;

    private void Awake() {
        combatEvent = GetComponent<CombatEvent>();
    }

    private void OnEnable() {
        combatEvent.OnDamageEvent += CombatEvent_OnDamageEvent;
    }

    private void OnDisable() {

        combatEvent.OnDamageEvent += CombatEvent_OnDamageEvent;
    }

    private void CombatEvent_OnDamageEvent(CombatEvent @event, DamageEventArg arg) {

        if (isDamageImmunity) {
            return;
        }

        isDamageImmunity = true;

        Vector2 damageDirection = arg.damageDirection;
        float knockBackThrust = arg.knockBackThrust;
        int damageAmount = arg.damageAmount;

        StartCoroutine(ResetDamageImmunityRoutine(damageDirection, damageAmount, knockBackThrust));
    }


    private IEnumerator ResetDamageImmunityRoutine(Vector2 damageDirection, int damageAmount, float knockBackThrust) {


        if (TryGetComponent<Health>(out var health)) {
            health.TakeDamage(damageDirection,knockBackThrust,damageAmount);
        }

        yield return new WaitForSeconds(damageImmunityTimeMax);
        isDamageImmunity = false;
    }

}
