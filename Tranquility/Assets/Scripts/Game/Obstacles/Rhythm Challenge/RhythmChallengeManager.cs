// This class is single-handily the reason why I'm refraining from making
// the project open source.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmChallengeManager : MonoBehaviour {
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject[] pads;
    [SerializeField] private GameObject[] levelIndicators;
    [SerializeField] private GameObject barricade;

    public int level = 1;
    private bool isPlayerGoing;
    private int playerStep = 1;
    private bool hasPlayedPatternInit = false;

    private int[] patternOne;
    private int[] playerPatternOne;
    private int[] patternTwo;
    private int[] playerPatternTwo;
    private int[] patternThree;
    private int[] playerPatternThree;

    void Start() {
        patternOne = CreatePattern(3);
        playerPatternOne = new int[3];
        patternTwo = CreatePattern(5);
        playerPatternTwo = new int[5];
        patternThree = CreatePattern(7);
        playerPatternThree = new int[7];
    }

    private void Update() {
        if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < 6 && !hasPlayedPatternInit) {
            hasPlayedPatternInit = true;
            StartCoroutine("PlayPattern");
        }
    }

    private int[] CreatePattern(int lights) {
        int[] pattern = new int[lights];
        for (int i = 0; i < pattern.Length; i++) {
            pattern[i] = UnityEngine.Random.Range(0, 4);
        }
        return pattern;
    }

    private IEnumerator PlayPattern() {
        yield return new WaitForSeconds(2.5f);
        int counter = 0;
        switch (level) {
            case 1:
                {
                    while (counter < patternOne.Length) {
                        pads[patternOne[counter]].transform.GetChild(1).gameObject.SetActive(true);
                        yield return new WaitForSeconds(0.5f);
                        pads[patternOne[counter]].transform.GetChild(1).gameObject.SetActive(false);
                        yield return new WaitForSeconds(0.25f);
                        counter++;
                    }
                    break;
                }
            case 2:
                {
                    while (counter < patternTwo.Length) {
                        pads[patternTwo[counter]].transform.GetChild(1).gameObject.SetActive(true);
                        yield return new WaitForSeconds(0.5f);
                        pads[patternTwo[counter]].transform.GetChild(1).gameObject.SetActive(false);
                        yield return new WaitForSeconds(0.25f);
                        counter++;
                    }
                    break;
                }
            case 3:
                {
                    while (counter < patternThree.Length) {
                        pads[patternThree[counter]].transform.GetChild(1).gameObject.SetActive(true);
                        yield return new WaitForSeconds(0.5f);
                        pads[patternThree[counter]].transform.GetChild(1).gameObject.SetActive(false);
                        yield return new WaitForSeconds(0.25f);
                        counter++;
                    }
                    break;
                }
            default:
                break;
        }
    }

    private float GetDistanceFromPlayer() {
        return Vector2.Distance(transform.position, playerObject.transform.position);
    }

    public void AddLightToPlayerPattern(int lightID) {
        switch (level) {
            case 1:
                playerPatternOne[playerStep - 1] = lightID;
                break;
            case 2:
                playerPatternTwo[playerStep - 1] = lightID;
                break;
            case 3:
                playerPatternThree[playerStep - 1] = lightID;
                break;
            default:
                return;
        }
        if (playerStep == (level * 2 + 1)) {
            if (DoPatternsMatch()) {
                AdvancePuzzle();
            } else {
                StartCoroutine("FlickerRedLight");
            }
            StartCoroutine("PlayPattern");
            playerStep = 1;
            Array.Clear(playerPatternOne, 0, 3);
            Array.Clear(playerPatternTwo, 0, 5);
            Array.Clear(playerPatternThree, 0, 7);
        } else {
            playerStep++;
        }
    }

    private bool DoPatternsMatch() {
        int[] pattern1;
        int[] pattern2;

        switch (level) {
            case 1:
                {
                    pattern1 = patternOne;
                    pattern2 = playerPatternOne;
                }
                break;
            case 2:
                {
                    pattern1 = patternTwo;
                    pattern2 = playerPatternTwo;
                }
                break;
            case 3:
                {
                    pattern1 = patternThree;
                    pattern2 = playerPatternThree;
                }
                break;
            default:
                return false;
        }

        for (int i = 0; i < pattern1.Length; i++) {
            if (pattern1[i] != pattern2[i])
                return false;
        }

        return true;
    }

    private void AdvancePuzzle() {
        levelIndicators[level - 1].GetComponent<SpriteRenderer>().color = Color.white;
        if (level == 3)
            Destroy(barricade);
        level++;
    }

    private IEnumerator FlickerRedLight() {
        levelIndicators[level - 1].GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1.5f);
        levelIndicators[level - 1].GetComponent<SpriteRenderer>().color = Color.black;
    }


}
