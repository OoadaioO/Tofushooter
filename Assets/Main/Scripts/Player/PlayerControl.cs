using UnityEngine;

public class PlayerControl : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;

    private Player player;

    private Vector2 moveDirection;

    private void Awake() {
        player = GetComponent<Player>();
    }


    private void Update() {
        GatherInput();
        MovementInput();
    }



    private void GatherInput() {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector2(horizontal, vertical).normalized;

    }

    private void MovementInput() {

        if (moveDirection != Vector2.zero) {
            player.MovementByVelocityEvent.CallMovementByVelocityEvent(moveDirection, moveSpeed);
        } else {
            player.IdleEvent.CallIdleEvent();
        }
    }
}
