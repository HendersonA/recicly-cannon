using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Text textUI;

    void Start()
    {
        textUI = GetComponent<Text>();
    }

    private void UIText(float textScore)
    {
        textUI.text = "Gold: " + textScore.ToString() + "$";
    }

    void OnEnable()
    {
        // ScoreManager.OnScore += UIText;
    }
}