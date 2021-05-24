using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    [SerializeField] private GameObject keyUI;

    public static bool hasBeenPickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        hasBeenPickedUp = true;
        keyUI.SetActive(true);
        Destroy(gameObject);
    }
}
