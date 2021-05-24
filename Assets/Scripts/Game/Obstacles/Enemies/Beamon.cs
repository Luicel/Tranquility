using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beamon : MonoBehaviour {
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown;
    [SerializeField] private float fireRange;

    private float timeSinceLastFire;
    
    private void Update() {
        timeSinceLastFire += Time.deltaTime;
        if (GetDistanceFromPlayer() <= fireRange && timeSinceLastFire >= fireCooldown) {
            timeSinceLastFire = 0;
            ShootProjectileAtPlayer();
        }

        UpdateDirection();
    }

    private void ShootProjectileAtPlayer() {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }

    private float GetDistanceFromPlayer() {
        return Vector2.Distance(transform.position, playerObject.transform.position);
    }

    private void UpdateDirection() {
        if (GetDistanceFromPlayer() < fireRange) {
            if ((playerObject.transform.position.x - transform.position.x) < 0) {
                if (transform.localScale.x == 1.75f)
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            } else if ((playerObject.transform.position.x - transform.position.x) > 0) {
                if (transform.localScale.x == -1.75f)
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); 
            }
        }
    }

    // best way I could think of allowing the player to die on touch but also allowing the
    // projectile to not destroy on spawn. I need to do a better job with colliders...
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
            playerObject.GetComponent<PlayerShattering>().Shatter();
    }
}
