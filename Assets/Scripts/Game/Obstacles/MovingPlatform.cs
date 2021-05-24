// Trigger idea for keeping player on platform inspired by:
// https://answers.unity.com/questions/12083/how-to-get-a-character-to-move-with-a-moving-platf.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isMovingSideToSide;
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    [SerializeField] private float upBound;
    [SerializeField] private float downBound;
    private bool isMovingRight = true;
    private bool isMovingDown = true;

    private GameObject target = null;
    private Vector3 offset;

    private void Start() {
        target = null;

        if (isMovingSideToSide) {
            leftBound += transform.position.x;
            rightBound += transform.position.x;
        } else {
            upBound += transform.position.y;
            downBound += transform.position.y;
        }
    }

    private void Update() {
        if (isMovingSideToSide) {
            if (isMovingRight) {
                if (transform.position.x < rightBound) {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                } else {
                    isMovingRight = false;
                }
            } else {
                if (transform.position.x > leftBound) {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                } else {
                    isMovingRight = true;
                }
            }
        } else {
            if (isMovingDown) {
                if (transform.position.y > downBound) {
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                } else {
                    isMovingDown = false;
                }
            } else {
                if (transform.position.y < upBound) {
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                } else {
                    isMovingDown = true;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D col) {
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }

    void OnCollisionExit2D(Collision2D col) {
        target = null;
    }

    void LateUpdate() {
        if (target != null)
            target.transform.position = transform.position + offset;
    }
}
