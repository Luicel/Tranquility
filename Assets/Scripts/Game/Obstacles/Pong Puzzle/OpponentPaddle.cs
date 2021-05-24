using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentPaddle : MonoBehaviour {
    public float speed;
    [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] private GameObject ball;

    private new Rigidbody2D rigidbody2D;
    private Vector2 velocity = Vector2.zero;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (ball.transform.position.y > transform.position.y) {
            rigidbody2D.velocity = Vector2.SmoothDamp(rigidbody2D.velocity, Vector2.up * speed, ref velocity, movementSmoothing);
        } else {
            rigidbody2D.velocity = Vector2.SmoothDamp(rigidbody2D.velocity, Vector2.down * speed, ref velocity, movementSmoothing);

        }
    }
}
