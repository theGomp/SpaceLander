﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUp : MonoBehaviour {

    private Text text;                              // Initialize the text
    public static bool secondTime;                         // prevents popup from opening multiple times

    void Start() {
        if (!secondTime)
        {
            text = GetComponentInChildren<Text>();      // Get the text component within the children
            if (text == null)
            {
                Debug.LogError("There is no Text component attached within the children");
            }
            Time.timeScale = 0;                         // No gravity or force or controls will be available.
        }
        else
            gameObject.SetActive(false);            // Disable the gameObject
    }

    void Update() {
        if (Input.anyKeyDown) {                     // When any key is pressed and let go...
            Time.timeScale = 1;                     // Gravity, force, and controls can be applied.
            gameObject.SetActive(false);            // Disable the gameObject
            secondTime = true;
        }
    }
}