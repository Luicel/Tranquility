using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobOfNothingness : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float alertRange;
    [SerializeField] private float followRange;
    [SerializeField] private float movementSmoothing = 0.05f;
    private Vector2 velocity = Vector2.zero;
    private Vector3 startingPosition;
    private bool isMoving = false;

    public GameObject playerObject;
    private new Rigidbody2D rigidbody2D;

    private void Awake() {
        startingPosition = transform.position;

        rigidbody2D = GetComponent<Rigidbody2D>();

        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
    }

    private void Update() {
        if (getDistanceFromPlayer() <= alertRange && !isMoving)
            StartCoroutine("FollowPlayer");
    }

    private IEnumerator FollowPlayer() {
        Vector2 positionCache = new Vector2(0f, 0f);
        float timePassedCache = 0f;

        Vector2 targetVelocity;

        isMoving = true;
        while (isMoving) {
            yield return null;
            // Move the object to their designated goal
            if (getDistanceFromPlayer() <= followRange && isPlayerAlive()) {
                targetVelocity = (playerObject.transform.position - transform.position).normalized * speed;
                rigidbody2D.velocity = Vector2.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
            } else {
                targetVelocity = (startingPosition - transform.position).normalized * speed;
                rigidbody2D.velocity = Vector2.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
                if (Vector2.Distance(transform.position, startingPosition) <= 0.01f)
                    isMoving = false;
            }
            // Check to see if object is stuck. If so, return to startingPosition and stop moving
            if (Vector2.Distance(transform.position, positionCache) <= 1f) {
                timePassedCache += Time.deltaTime;
                if (timePassedCache >= 10f) {
                    transform.position = startingPosition;
                    isMoving = false;
                }
            } else {
                positionCache = transform.position;
                timePassedCache = 0f;
            }
        }

        rigidbody2D.velocity = Vector2.zero;
    }

    private float getDistanceFromPlayer() {
        return Vector2.Distance(transform.position, playerObject.transform.position);
    }

    private bool isPlayerAlive() {
        return playerObject.GetComponent<SpriteRenderer>().enabled;
    }
}
