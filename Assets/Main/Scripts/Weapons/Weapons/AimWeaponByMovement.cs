using System;
using UnityEngine;

[DisallowMultipleComponent]
public class AimWeaponByMovement : MonoBehaviour {
    [SerializeField] private Transform aimRotationTransform;
    [SerializeField] private float rotateSpeed;

    private MovementByVelocityEvent movementByVelocityEvent;
    private Vector2 moveDirection;

    private void Awake() {
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
    }

    private void OnEnable() {
        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void OnDisable() {
        movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void Update() {
        aimRotationTransform.right = Vector3.Slerp(aimRotationTransform.right, moveDirection, Time.deltaTime * rotateSpeed);
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityArgs movementByVelocityArgs) {
        moveDirection = movementByVelocityArgs.moveDirection;
    }
}
