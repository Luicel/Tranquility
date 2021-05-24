// This file is inspired by Brackleys's tutorial on Save and Load Systems
// https://www.youtube.com/watch?v=XOjd_qU2Ido

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointPositionData
{
    public float[] checkpointLocation;

    public CheckpointPositionData(Vector2 position) {
        checkpointLocation = new float[2];
        checkpointLocation[0] = position.x;
        checkpointLocation[1] = position.y;
    }
}
