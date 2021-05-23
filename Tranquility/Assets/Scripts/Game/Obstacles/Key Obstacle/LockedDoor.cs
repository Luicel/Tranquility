using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour {
    [SerializeField] private GameObject keyUI;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && Key.hasBeenPickedUp) {
            keyUI.SetActive(false);
            Destroy(gameObject);
        }
    }
}
