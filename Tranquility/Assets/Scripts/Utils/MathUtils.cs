using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils {
    public static float Get360DegreeAngleBetween(Vector2 vector1, Vector2 vector2, bool isYBelowZero) {
        float angle = Vector2.Angle(vector1, vector2);
        if (isYBelowZero)
            angle = (180 - angle) + 180;

        return angle;
    }
}
