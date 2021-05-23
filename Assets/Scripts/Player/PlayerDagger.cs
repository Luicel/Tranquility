using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDagger : MonoBehaviour {
    public GameObject daggerHitbox;
    [SerializeField] private float daggerOutTime;

    private ControlsUtils.CardinalDirection cardinalDirection;
    private float positionMultiplier = 1f;

    private PlayerControls controls;
    private Vector2 direction;

    void Awake() {
        cardinalDirection = ControlsUtils.CardinalDirection.NONE;

        controls = new PlayerControls();

        // directional (aka movement) controls registration (InputSystem)
        controls.Gameplay.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => direction = Vector2.zero;
        // swiping controls registration (InputSystem)
        controls.Gameplay.Dagger.performed += ctx => Swipe();
    }

    private void Swipe() {
        if (daggerHitbox.gameObject.activeSelf)
            return;

        float angle = MathUtils.Get360DegreeAngleBetween(new Vector2(1, 0), direction, (direction.y < 0));
        cardinalDirection = ControlsUtils.GetCardinalDirection(angle);
        if (!PlayerAnimations.isFacingRight)
            cardinalDirection = ControlsUtils.ReflectCardinalDirectionVertically(cardinalDirection);

        StartCoroutine("ActivateDagger");
    }

    private void PositionHitBox(ControlsUtils.CardinalDirection cardinalDirection) {
        Vector2 playerOffset = transform.position;

        switch (cardinalDirection) {
            case ControlsUtils.CardinalDirection.N:
                daggerHitbox.transform.position = new Vector2(0f * positionMultiplier, 1f * positionMultiplier) + playerOffset;
                break;
            case ControlsUtils.CardinalDirection.NE:
                daggerHitbox.transform.position = new Vector2(0.7f * positionMultiplier, 0.7f * positionMultiplier) + playerOffset;
                break;
            case ControlsUtils.CardinalDirection.E:
                daggerHitbox.transform.position = new Vector2(1f * positionMultiplier, 0f * positionMultiplier) + playerOffset;
                break;
            case ControlsUtils.CardinalDirection.SE:
                daggerHitbox.transform.position = new Vector2(0.7f * positionMultiplier, -0.7f * positionMultiplier) + playerOffset;
                break;
            case ControlsUtils.CardinalDirection.S:
                daggerHitbox.transform.position = new Vector2(0 * positionMultiplier, -1f * positionMultiplier) + playerOffset;
                break;
            case ControlsUtils.CardinalDirection.SW:
                daggerHitbox.transform.position = new Vector2(-0.7f * positionMultiplier, -0.7f * positionMultiplier) + playerOffset;
                break;
            case ControlsUtils.CardinalDirection.W:
                daggerHitbox.transform.position = new Vector2(-1f * positionMultiplier, 0f * positionMultiplier) + playerOffset;
                break;
            case ControlsUtils.CardinalDirection.NW:
                daggerHitbox.transform.position = new Vector2(-0.7f * positionMultiplier, 0.7f * positionMultiplier) + playerOffset;
                break;
            default:
                daggerHitbox.transform.position = new Vector2(0f * positionMultiplier, 0f * positionMultiplier) + playerOffset;
                break;
        }
    }

    private IEnumerator ActivateDagger() {
        daggerHitbox.gameObject.SetActive(true);
        float timePassed = 0f;

        while (timePassed < daggerOutTime) {
            timePassed += Time.deltaTime;
            yield return null;
        }

        daggerHitbox.gameObject.SetActive(false);
        cardinalDirection = ControlsUtils.CardinalDirection.NONE;
    }

    private void LateUpdate() {
        PositionHitBox(cardinalDirection);
    }

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    private void OnDisable() {
        controls.Gameplay.Disable();
    }
}
