using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranquilityBell : MonoBehaviour {
    [SerializeField] private GameObject UIObject;
    [SerializeField] private Sprite fullSprite;


    public void SetPieceToVisible(int id) {
        transform.GetChild(id).gameObject.SetActive(true);
        UIObject.transform.GetChild(id).gameObject.SetActive(true);

        if (AreAllPiecesCollected())
            ChangeBellsToFull();
    }

    private bool AreAllPiecesCollected() {
        foreach (Transform transform in transform) {
            if (!transform.gameObject.activeSelf)
                return false;
        }
        return true;
    }
    
    private void ChangeBellsToFull() {
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = fullSprite;
        UIObject.GetComponent<Image>().sprite = fullSprite;
    }
}
