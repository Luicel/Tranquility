// This file is inspired by Brackleys's tutorial on Save and Load Systems
// https://www.youtube.com/watch?v=XOjd_qU2Ido

using UnityEngine;

[System.Serializable]
public class PiecesCollectedData {
    public bool[] collectedPieces;

    // Array of 5 elements for each 5 pieces's IDs. True means collected.
    public PiecesCollectedData(bool[] collectedPieces) {
        collectedPieces = new bool[5];
        this.collectedPieces[0] = collectedPieces[0];
        this.collectedPieces[1] = collectedPieces[1];
        this.collectedPieces[2] = collectedPieces[2];
        this.collectedPieces[3] = collectedPieces[3];
        this.collectedPieces[4] = collectedPieces[4];
    }
}
