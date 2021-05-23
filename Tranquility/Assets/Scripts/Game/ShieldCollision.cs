using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour {
    [SerializeField] private GameObject playerObject;
    private PlayerShield shieldScript;

    private void Awake() {
        shieldScript = playerObject.GetComponent<PlayerShield>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Damager"))
            HandleShieldCollision(collision);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Damager"))
            HandleShieldCollision(collision);
    }

    private void HandleShieldCollision(Collider2D collision) {
        Vector2 collisionDirection = (collision.transform.position - transform.position).normalized;
        float collisionAngle = MathUtils.Get360DegreeAngleBetween(new Vector2(1, 0), collisionDirection, (collisionDirection.y < 0));

        Vector2 shieldDirection = shieldScript.getDirection();
        float shieldAngle = MathUtils.Get360DegreeAngleBetween(new Vector2(1, 0), shieldDirection, (shieldDirection.y < 0));

        float shieldAngleLeftOffset = shieldAngle - ((shieldScript.getMaxDegrees() / 2)) * (shieldScript.getStamina() / 100);
        float shieldAngleRightOffset = shieldAngle + ((shieldScript.getMaxDegrees() / 2)) * (shieldScript.getStamina() / 100);

        if (isAngleWithinBounds(collisionAngle, shieldAngleLeftOffset, shieldAngleRightOffset)) {
            Destroy(collision.gameObject);
            shieldScript.setStamina(shieldScript.getStamina() - 30);
        }
    }

    private bool isAngleWithinBounds(float angle, float leftBound, float rightBound) {
        if (angle >= leftBound && angle <= rightBound) {
            return true;
        } else if (angle >= leftBound + 360 && angle <= rightBound + 360) {
            if (leftBound < 0 || rightBound > 360) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
}
