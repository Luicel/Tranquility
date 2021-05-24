using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TranquilityBell : MonoBehaviour {
    [SerializeField] private GameObject UIObject;
    [SerializeField] private Sprite fullSprite;
    [SerializeField] private Animator fadeAnimator;

    public void SetUIPieceToVisible(int id)
    {
        UIObject.transform.GetChild(id).gameObject.SetActive(true);

        if (AreAllPiecesCollected()) {
            UIObject.GetComponent<Image>().sprite = fullSprite;
            StartCoroutine("GoToEpilogue");
        }
    }

    public void SetPhysicalPieceToVisible(int id) {
        transform.GetChild(id).gameObject.SetActive(true);

        if (AreAllPiecesCollected()) {
            GetComponent<SpriteRenderer>().sprite = fullSprite;
        }
    }

    private bool AreAllPiecesCollected() {
        foreach (Transform transform in UIObject.transform) {
            if (!transform.gameObject.activeSelf)
                return false;
        }
        return true;
    }

    private IEnumerator GoToEpilogue() {
        yield return new WaitForSeconds(1f);
        fadeAnimator.SetTrigger("fadeOut");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Epilogue");
    }
}
