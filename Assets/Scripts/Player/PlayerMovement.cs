using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    public float thresholdMin;
    public float thresholdMax;

    private PlayerController controller;
    private PlayerControls controls;
    private Vector2 movement;
    private bool isJumping;

    void Awake() {
        controller = GetComponent<PlayerController>();
        controls = new PlayerControls();

        // movement controls registration
        controls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
        // jumping controls registration
        controls.Gameplay.Jump.performed += ctx => Jump();
    }

    private void FixedUpdate() {
        ApplyThresholdLimitationsToMovement();

        controller.Move(movement.x, isJumping);
        isJumping = false;
    }

    private void ApplyThresholdLimitationsToMovement() {
        if ((movement.x <= thresholdMin && movement.x >= 0) || (movement.x >= -thresholdMin && movement.x <= 0))
            movement.x = 0;
        else if (movement.x >= thresholdMax || movement.x <= -thresholdMax)
            movement.x = (movement.x > 0) ? thresholdMax : -thresholdMax;
    }

    private void Jump() {
        isJumping = true;
    }

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    private void OnDisable() {
        controls.Gameplay.Disable();
    }
}
