using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamonProjectile : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float movementSmoothing = 0.05f;
    private Vector3 targetVelocity;
    private Vector2 velocity = Vector2.zero;
    private Vector3 targetLocation;

    private new Rigidbody2D rigidbody2D;

    private void Start() {
        // can't use instance as a variable so this is the next best thing
        targetLocation = GameObject.Find("Player").transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();

        targetVelocity = (targetLocation - transform.position).normalized * speed;
    }

    private void Update() {
        rigidbody2D.velocity = Vector2.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
    }

    // really poor way to fix an issue - tbh this shouldn't work, but it does
    // if isTrigger is off, it will just Destroy() when spawned inside the Beamon,
    // and isTrigger needs to be off for Damager detection
    private void OnTriggerEnter2D(Collider2D collision) {
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
