using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour {
    [SerializeField] private GameObject shieldObject;

    private float maxDegrees = 135f;
    private float stamina = 100f;

    private PlayerControls controls;
    private Vector2 direction;

    void Awake() {
        controls = new PlayerControls();

        // directional controls registration
        controls.Gameplay.Shield.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.Gameplay.Shield.canceled += ctx => direction = Vector2.zero;
    }

    private void Start() {
        StartCoroutine("HandleStamina");
    }

    void Update() {
        shieldObject.transform.position = transform.position;

        UpdateFill();

        if (IsPastThreshold(0.6f)) {
            UpdateDirection();
            SetVisibility(true);
        } else {
            SetVisibility(false);
        }
    }

    private void UpdateFill() {
        Image image = shieldObject.GetComponent<Image>();
        image.fillAmount = (stamina / 100) * (maxDegrees / 360);
    }

    private bool IsPastThreshold(float threshold) {
        if (direction.x >= threshold || direction.x <= -threshold)
            return true;
        else if (direction.y >= threshold || direction.y <= -threshold)
            return true;
        else
            return false;
    }

    private void SetVisibility(bool visibility) {
        shieldObject.SetActive(visibility);
    }

    private void UpdateDirection() {
        float angle = Vector2.Angle(Vector2.right, direction);
        if (direction.y < 0)
            angle = (180 - angle) + 180;

        if (stamina > 0)
            shieldObject.transform.rotation = Quaternion.Euler(0, 0, angle + ((stamina / 100) * (maxDegrees / 2)));
    }

    private IEnumerator HandleStamina() {
        while (true) {
            yield return new WaitForSeconds(0.05f);

            if (direction == Vector2.zero) {
                if (stamina < 100)
                    stamina++;
            } else {
                if (stamina > 0)
                    stamina--;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // TODO detect collision entrance point, compare angle to shield length/angle, and handle accordingly

        bool hitShield = DidObjectHitShield(collision.transform.position);
    }

    private bool DidObjectHitShield(Vector2 position) {
        float angle = Vector2.Angle(Vector2.right, position);
        if (direction.y < 0)
            angle = (180 - angle) + 180;



        return false;
    }

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    private void OnDisable() {
        controls.Gameplay.Disable();
    }
}
