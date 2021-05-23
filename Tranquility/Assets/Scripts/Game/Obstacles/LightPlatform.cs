using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlatform : MonoBehaviour {
    private BoxCollider2D boxCollider2D;

    private PlayerController controller;
    private PlayerControls controls;
    private Vector2 movement;

    void Awake()  {
        boxCollider2D = GetComponent<BoxCollider2D>();

        controller = GetComponent<PlayerController>();
        controls = new PlayerControls();

        // movement controls registration
        controls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
    }

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    private void OnDisable() {
        controls.Gameplay.Disable();
    }
}
