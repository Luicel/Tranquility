using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlsUtils {
    public enum CardinalDirection { N, NE, E, SE, S, SW, W, NW, NONE };

    public static CardinalDirection GetCardinalDirection(float angle) {
        CardinalDirection cardinalDirection;

        if (angle <= 22.5 || angle >= 337.5)
            cardinalDirection = CardinalDirection.E;
        else if (angle <= 67.5 && angle >= 22.5)
            cardinalDirection = CardinalDirection.NE;
        else if (angle <= 112.5 && angle >= 67.5)
            cardinalDirection = CardinalDirection.N;
        else if (angle <= 157.5 && angle >= 112.5)
            cardinalDirection = CardinalDirection.NW;
        else if (angle <= 202.5 && angle >= 157.5)
            cardinalDirection = CardinalDirection.W;
        else if (angle <= 247.5 && angle >= 202.5)
            cardinalDirection = CardinalDirection.SW;
        else if (angle <= 292.5 && angle >= 247.5)
            cardinalDirection = CardinalDirection.S;
        else if (angle <= 337.5 && angle >= 292.5)
            cardinalDirection = CardinalDirection.SE;
        else
            cardinalDirection = CardinalDirection.NONE;

        if (!PlayerAnimations.isFacingRight)
            cardinalDirection = ReflectCardinalDirectionVertically(cardinalDirection);

        return cardinalDirection;
    }

    public static CardinalDirection ReflectCardinalDirectionVertically(CardinalDirection cardinalDirection) {
        switch (cardinalDirection) {
            case CardinalDirection.NE:
                cardinalDirection = CardinalDirection.NW;
                break;
            case CardinalDirection.E:
                cardinalDirection = CardinalDirection.W;
                break;
            case CardinalDirection.SE:
                cardinalDirection = CardinalDirection.SW;
                break;
            case CardinalDirection.SW:
                cardinalDirection = CardinalDirection.SE;
                break;
            case CardinalDirection.W:
                cardinalDirection = CardinalDirection.E;
                break;
            case CardinalDirection.NW:
                cardinalDirection = CardinalDirection.NE;
                break;
            default:
                break;
        }

        return cardinalDirection;
    }

    public static bool IsDirectionPastThreshold(Vector2 direction, float threshold) {
        if (direction.x >= threshold || direction.x <= -threshold)
            return true;
        else if (direction.y >= threshold || direction.y <= -threshold)
            return true;
        else
            return false;
    }
}
