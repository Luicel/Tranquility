// The InputSystem code (and the fact that there's different action maps for each
// menu) is hacky but I had no choice due to an InputSystem issue. It refused to
// let me utilize C# events to remove context from an action - this is either an
// issue with how unexperienced I am or an actual bug with InputSystem itself.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlsScreen : MonoBehaviour {
    [SerializeField] private GameObject continueTextSection;

    private PlayerControls controls;

    private float timeElapsed;

    private void Awake() {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Continue();
    }

    private void Continue() {
        SceneManager.LoadScene("Prologue");
    }

    private void Update() {
        BlinkContinueText();
    }

    private void BlinkContinueText() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed % 1 > 0.5)
            continueTextSection.SetActive(false);
        else
            continueTextSection.SetActive(true);
    }

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    private void OnDisable() {
        controls.Gameplay.Disable();
    }
}
