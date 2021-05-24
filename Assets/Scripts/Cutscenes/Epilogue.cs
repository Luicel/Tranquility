// featuring very scuffed responsive UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Epilogue : MonoBehaviour {
    [SerializeField] private float timeBetweenMove;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject textBlocker;

    private void Start() {
        StartCoroutine("PlayEpilogue");
    }
    
    private IEnumerator PlayEpilogue() {
        float distance;
        while (true) {
            yield return new WaitForSeconds(timeBetweenMove);
            distance = Screen.height / 13.5f;
            textBlocker.transform.Translate(Vector3.down * distance);
            if (textBlocker.transform.position.y <= Screen.height - (distance * 18))
                break;
        }
        yield return new WaitForSeconds(6f);
        distance = Screen.height / 13.5f;
        textBlocker.transform.Translate(Vector3.up * distance * 12);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TitleScreen");
    }
}
