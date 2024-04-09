using UnityEngine;

[RequireComponent(typeof(AimWeaponEvent))]
public class AimWeapon : MonoBehaviour {
    [Tooltip("Populate with the Transform from the child WeaponRotationPoint gameObject")]
    [SerializeField] private Transform weaponRotationPointTransform;


    private AimWeaponEvent aimWeaponEvent;

    private void Awake() {
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
    }

    private void OnEnable() {
        aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
    }
    private void OnDisable() {
        aimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
    }

    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs aimWeaponEventArgs) {
        Aim(aimWeaponEventArgs.aimDirection, aimWeaponEventArgs.aimAngle);
    }

    private void Aim(AimDirection aimDirection, float aimAngle) {
        weaponRotationPointTransform.eulerAngles = new Vector3(0, 0, aimAngle);

        switch (aimDirection) {
            case AimDirection.UpLeft:
            case AimDirection.Left:
                weaponRotationPointTransform.localScale = new Vector3(1f, -1f, 0f);
                break;

            case AimDirection.UpRight:
            case AimDirection.Right:
                weaponRotationPointTransform.localScale = new Vector3(1f, 1f, 0f);
                break;
            case AimDirection.Down:
                weaponRotationPointTransform.localScale = new Vector3(1f, 1f, 0f);
                break;
            case AimDirection.Up:
                if (aimAngle >= 90) {
                    weaponRotationPointTransform.localScale = new Vector3(1f, -1f, 0f);
                } else {
                    weaponRotationPointTransform.localScale = new Vector3(1f, 1f, 0f);
                    break;
                }
                break;
        }
    }


}
