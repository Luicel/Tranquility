using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmPad : MonoBehaviour {
    [SerializeField] private int id;

    private bool isLit = false;

    private RhythmChallengeManager rhythmChallengeScript;

    private void Awake() {
        rhythmChallengeScript = GameObject.Find("Rhythm Challenge Manager").GetComponent<RhythmChallengeManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!isLit) {
            rhythmChallengeScript.AddLightToPlayerPattern(id);
            StartCoroutine("Lighting");
        }
    }

    private IEnumerator Lighting() {
        isLit = true;
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isLit = false;
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
