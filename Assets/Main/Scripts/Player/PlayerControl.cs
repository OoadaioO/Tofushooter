using Unity.Mathematics;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 3f;

    private Player player;

    private Vector2 moveDirection;
    private Vector2 weaponDirection = Vector2.right;

    private void Awake() {
        player = GetComponent<Player>();
    }


    private void Update() {
        GatherInput();
        MovementInput();
        AimWeaponInput();
    }



    private void GatherInput() {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector2(horizontal, vertical).normalized;



    }

    private void MovementInput() {

        if (moveDirection != Vector2.zero) {
            player.movementByVelocityEvent.CallMovementByVelocityEvent(moveDirection, moveSpeed);
        } else {
            player.idleEvent.CallIdleEvent();
        }
    }

    private void AimWeaponInput() {

        if (moveDirection == Vector2.zero) return;


        float angle = Vector3.SignedAngle(weaponDirection, moveDirection,Vector3.forward);
        Vector3 newDirection = Quaternion.AngleAxis(angle * Time.deltaTime * rotateSpeed, Vector3.forward) * (Vector3)weaponDirection;
        weaponDirection = newDirection;


        float weaponAngleDegrees = HelperUtilities.GetAngleFromVector(weaponDirection);
        AimDirection weaponAimDirection = HelperUtilities.GetAimDirection8(weaponAngleDegrees);


        Vector2 playerDirection = moveDirection;
        float playerAngleDegrees = HelperUtilities.GetAngleFromVector(playerDirection);
        AimDirection playerAimDirection = HelperUtilities.GetAimDirection8(playerAngleDegrees);


        player.aimWeaponEvent.CallAimWeaponEvent(playerAimDirection, playerAngleDegrees, weaponAimDirection, weaponAngleDegrees, weaponDirection, true);

    }


}
