using UnityEngine;


[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour {
    private Player player;
    private void Awake() {
        player = GetComponent<Player>();
    }
    private void OnEnable() {
        player.movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;

        player.idleEvent.OnIdle += IdleEvent_OnIdle;
        player.aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
    }
    private void OnDisable() {

        player.movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        player.idleEvent.OnIdle -= IdleEvent_OnIdle;
        player.aimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
    }


    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityArgs movementByVelocityArgs) {
        SetMovementAnimationParameters();
    }


    private void IdleEvent_OnIdle(IdleEvent idleEvent) {
        SetIdleAnimationParameters();

    }

    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs args) {
        SetAimWeaponAnimationParamters(args.aimDirection);
    }


    private void SetMovementAnimationParameters() {
        player.animator.SetBool(Settings.isMoving, true);
        player.animator.SetBool(Settings.isIdle, false);
    }


    private void SetIdleAnimationParameters() {
        player.animator.SetBool(Settings.isMoving, false);
        player.animator.SetBool(Settings.isIdle, true);
    }

    private void SetAimWeaponAnimationParamters(AimDirection aimDirection) {
        player.animator.SetInteger(Settings.aimDirection, (int)aimDirection);
    }
}
