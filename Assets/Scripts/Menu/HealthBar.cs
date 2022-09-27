using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image currentHealthbar;
    [SerializeField] private Text ratioText;

    private void UpdateHealthBar(float hitpoint, float maxHitpoint)
    {
        float ratiobar = hitpoint / maxHitpoint;

        currentHealthbar.rectTransform.localScale = new Vector3(ratiobar, 1, 1);
        ratioText.text = "Health: " + (ratiobar * 100).ToString("0") + '%';
    }

    void OnEnable()
    {
        health.OnHealth += UpdateHealthBar;
    }
}