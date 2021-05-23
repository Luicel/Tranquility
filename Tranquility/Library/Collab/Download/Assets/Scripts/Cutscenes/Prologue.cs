using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour {
    [SerializeField] private float timeBetweenMove;
    [SerializeField] private GameObject textBlocker;

    private void Start() {
        StartCoroutine("PlayPrologue");
    }
    
    private IEnumerator PlayPrologue() {
        while (true) {
            yield return new WaitForSeconds(timeBetweenMove);
            textBlocker.transform.Translate(Vector3.down * 80);
            if (textBlocker.transform.position.y <= -360)
                break;
        }
        yield return new WaitForSeconds(6f);
        textBlocker.transform.Translate(Vector3.up * 800);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("RealmOfLight");
    }
}
