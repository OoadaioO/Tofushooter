using Unity.Mathematics;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 3f;

    private Player player;

    private Vector2 moveDirection = Vector3.down;
    private Vector2 weaponAimVector = Vector3.right;

    private bool isMoving = false;
    private bool isFirePreviousFrame;

    private void Awake() {
        player = GetComponent<Player>();
    }


    private void Update() {
        GatherInput();
        MovementInput();
        WeaponInput();
    }



    private void GatherInput() {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector2(horizontal, vertical).normalized;

    }

    private void MovementInput() {

        isMoving = moveDirection != Vector2.zero;


        if (isMoving) {
            player.movementByVelocityEvent.CallMovementByVelocityEvent(moveDirection, moveSpeed);
        } else {
            player.idleEvent.CallIdleEvent();
        }
    }

    private void WeaponInput() {

        AimWeaponInput(out Vector3 weaponDirection, out float weaponAngleDegrees, out float playerAngleDegrees, out AimDirection playerAimDirection);
        FireWeaponInput(weaponDirection, weaponAngleDegrees, playerAngleDegrees, playerAimDirection);
    }

    private void AimWeaponInput(out Vector3 weaponDirection, out float weaponAngleDegrees, out float playerAngleDegrees, out AimDirection playerAimDirection) {

        float angle = Vector3.SignedAngle(this.weaponAimVector, moveDirection, Vector3.forward);
        this.weaponAimVector = Quaternion.AngleAxis(angle * Time.deltaTime * rotateSpeed, Vector3.forward) * (Vector3)this.weaponAimVector; ;

        weaponDirection = this.weaponAimVector;
        weaponAngleDegrees = HelperUtilities.GetAngleFromVector(weaponDirection);
        playerAngleDegrees = weaponAngleDegrees;
        playerAimDirection = HelperUtilities.GetAimDirection8(weaponAngleDegrees);

        player.aimWeaponEvent.CallAimWeaponEvent(playerAimDirection, playerAngleDegrees, weaponAngleDegrees, weaponDirection);

    }

    private void FireWeaponInput(Vector3 weaponDirection, float weaponAngleDegrees, float playerAngleDegrees, AimDirection playerAimDirection) {


        player.fireWeaponEvent.CallFireWeaponEvent(true, isFirePreviousFrame, playerAimDirection, playerAngleDegrees, weaponAngleDegrees, weaponDirection);

    }


}
