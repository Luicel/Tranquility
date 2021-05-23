using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] private GameObject playerObject;
    private bool isMovingRight;
    private bool isMovingDown;

    private Vector2 velocity = Vector2.zero;

    private new Rigidbody2D rigidbody2D;

    private void Awake() {
        Physics2D.IgnoreCollision(playerObject.GetComponent<BoxCollider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(playerObject.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector3 targetVelocity = new Vector3((isMovingRight) ? 1 : -1, (isMovingDown) ? -1 : 1, 0) * speed;
        rigidbody2D.velocity = Vector2.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector3 vector = (new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0) - transform.position).normalized;
        float angle = MathUtils.Get360DegreeAngleBetween(Vector3.right, vector, (vector.y < 0));
        // Using ControlsUtils.CardinalDirection is very dirty, but it (miraculously) gets the job done :D
        ControlsUtils.CardinalDirection direction = ControlsUtils.GetCardinalDirection(angle);

        if (direction == ControlsUtils.CardinalDirection.N || direction == ControlsUtils.CardinalDirection.S)
            isMovingDown = !isMovingDown;
        else
            isMovingRight = !isMovingRight;
    }
}
