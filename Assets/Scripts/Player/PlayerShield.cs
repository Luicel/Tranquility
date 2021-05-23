using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour {
    public GameObject shieldObject;
    public Image staminaBarForeground;

    private float stamina = 100f;
    private float maxDegrees = 135;
    private PlayerControls controls;
    private Vector2 direction;

    void Awake() {
        controls = new PlayerControls();

        // directional controls registration (InputSystem)
        controls.Gameplay.Shield.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.Gameplay.Shield.canceled += ctx => direction = Vector2.zero;

        StartCoroutine("HandleStamina");
    }

    void Update() {
        shieldObject.transform.position = transform.position;

        if (ControlsUtils.IsDirectionPastThreshold(direction, 0.6f)) {
            float angle = MathUtils.Get360DegreeAngleBetween(new Vector2(1, 0), direction, (direction.y < 0));
            UpdateRotation(angle);
            UpdateFill();

            SetActivity(true);
            PlayerAnimations.hasArmOut = true;
            PlayerAnimations.UpdateCardinalDirection(angle);
        } else {
            SetActivity(false);
            PlayerAnimations.hasArmOut = false;
            PlayerAnimations.UpdateCardinalDirection(ControlsUtils.CardinalDirection.NONE);
        }

        UpdateStaminaBar();
    }

    private void SetActivity(bool isActive) {
        shieldObject.SetActive(isActive);
    }

    private void UpdateRotation(float angle) {
        shieldObject.transform.eulerAngles = new Vector3(
            shieldObject.transform.eulerAngles.x,
            shieldObject.transform.eulerAngles.y,
            angle + ((stamina / 100) * (maxDegrees / 2))
        );
    }

    private void UpdateFill() {
        Image image = shieldObject.GetComponent<Image>();
        image.fillAmount = (stamina / 100) * (maxDegrees / 360);
    }

    private void UpdateStaminaBar() {
        staminaBarForeground.fillAmount = stamina / 100;
    }

    private IEnumerator HandleStamina() {
        while (true) {
            yield return new WaitForSeconds(0.025f);

            if (shieldObject.activeSelf) {
                if (stamina > 0)
                    stamina -= 0.5f;
            } else {
                if (stamina < 100)
                    stamina += 0.25f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // TODO collision logic. I need a test object to flush this out, however I know how it will be done.
        // The shield will have a circle collider, and upon collision with another object the point of entry
        // will be recorded. Calculations will be done using stamina, maxDegrees, and the object's rotation
        // to determine if the collision hits the shield or not, and appropriate logic will be called as a
        // result.
    }

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    private void OnDisable() {
        controls.Gameplay.Disable();
    }
}
