using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    [HideInInspector] public static bool isFacingRight = true;

    [HideInInspector] public static float horizontalMovement;
    [HideInInspector] public static bool isJumping;
    [HideInInspector] public static bool isGrounded;
    [HideInInspector] public static bool hasArmOut;

    private static ControlsUtils.CardinalDirection cardinalDirection;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetFloat("Movement", ((horizontalMovement >= 0) ? horizontalMovement : -horizontalMovement));
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("HasArmOut", hasArmOut);

        ShowArm(cardinalDirection);
    }

    private void ShowArm(ControlsUtils.CardinalDirection cardinalDirection) {
        GameObject armSpriteObject = GameObject.Find("Arm Sprites");
        string armSpriteName = $"Player_Arm_Out_{cardinalDirection}";

        foreach (Transform child in armSpriteObject.transform) {
            if (child.name == armSpriteName)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
    }

    private void DisableAllArmsExcept(int exceptionIndex) {

    }

    public static void UpdateCardinalDirection(float angle) {
        cardinalDirection = ControlsUtils.GetCardinalDirection(angle);
    }

    public static void UpdateCardinalDirection(ControlsUtils.CardinalDirection cardinalDir) {
        cardinalDirection = cardinalDir;
    }
}
