// TODO list:
// - fix odd pathing behavior
// - respawn all enemies upon reset

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingPlatform : MonoBehaviour {
    [SerializeField] private GameObject[] points;
    [SerializeField] private float speed;

    private bool isMoving;
    private int nextPointIndex = 0;
    private Vector3 startingPosition;

    private void Awake() {
        startingPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && !isMoving && !IsPlatformAtEnding()) {
            isMoving = true;
            StartCoroutine("MoveThroughPath");
        }
    }

    private IEnumerator MoveThroughPath() {
        while (isMoving) {
            yield return null;
            if (nextPointIndex < points.Length) {
                if (Vector3.Distance(transform.position, points[nextPointIndex].transform.position) > 0.1f) {
                    transform.Translate((points[nextPointIndex].transform.position - transform.position).normalized * speed * Time.deltaTime);
                } else {
                    nextPointIndex++;
                }
            } else {
                isMoving = false;
            }
            if (IsPlayerDead()) {
                isMoving = false;
                yield return new WaitForSeconds(1.5f);
                transform.position = startingPosition;
            }
        }
        nextPointIndex = 0;
        if (IsPlatformAtEnding()) {
            yield return new WaitForSeconds(2.5f);
            transform.position = startingPosition;
        }
    }

    private bool IsPlatformAtEnding() {
        return Vector3.Distance(transform.position, points[points.Length - 1].transform.position) < 0.1f;
    }

    private bool IsPlayerDead() {
        return !GameObject.Find("Player").GetComponent<SpriteRenderer>().enabled;
    }
}
