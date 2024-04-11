using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class AnimateEnemy : MonoBehaviour {
    private Enemy enemy;

    private void Awake() {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable() {
        enemy.movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;
        enemy.idleEvent.OnIdle += IdleEvent_OnIdle;
        enemy.aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
    }


    private void OnDisable() {
        enemy.movementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;
        enemy.idleEvent.OnIdle -= IdleEvent_OnIdle;
        enemy.aimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
    }

    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs aimWeaponEventArgs) {

        SetAimWeaponAnimationParamters(aimWeaponEventArgs.aimDirection);
    }

    private void MovementToPositionEvent_OnMovementToPosition(MovementToPositionEvent movementToPositionEvent, MovementToPositionArgs movementToPositionArgs) {

        SetMovementAnimationParameters();
    }

    private void IdleEvent_OnIdle(IdleEvent @event) {
        SetIdleAnimationParameters();
    }

    private void SetMovementAnimationParameters() {
        enemy.animator.SetBool(Settings.isIdle, false);
        enemy.animator.SetBool(Settings.isMoving, true);
    }
    private void SetIdleAnimationParameters() {
        enemy.animator.SetBool(Settings.isIdle, true);
        enemy.animator.SetBool(Settings.isMoving, false);
    }

    private void SetAimWeaponAnimationParamters(AimDirection aimDirection) {

        enemy.animator.SetInteger(Settings.aimDirection, (int)aimDirection);
    }

}
