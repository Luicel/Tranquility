using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellPiece : MonoBehaviour {
    [SerializeField] private GameObject transquilityBellObject;
    [SerializeField] private int id;

    private TranquilityBell tranquilityBellScript;

    private void Awake() {
        tranquilityBellScript = transquilityBellObject.GetComponent<TranquilityBell>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            GetComponent<BoxCollider2D>().enabled = false;
            CollectBellPiece();
            StartCoroutine("GoToTranquilityBell");
        }
    }

    private void CollectBellPiece() {
        tranquilityBellScript.SetUIPieceToVisible(id);
    }

    private IEnumerator GoToTranquilityBell() {
        float speed = 10f;
        Vector2 direction;

        while (Vector2.Distance(transform.position, transquilityBellObject.transform.position) >= 0.1f) {
            direction = (transquilityBellObject.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            yield return null;
        }

        tranquilityBellScript.SetPhysicalPieceToVisible(id);
        Destroy(gameObject);
    }
}
