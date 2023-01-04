using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour
{
    public Slider _slider;
    [SerializeField] TMPro.TextMeshProUGUI levelText;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void UpdateEXPBar(int exp)
    {
        _slider.value = exp;
    }
    public void UpdateTotalEXPBar(int totalExp)
    {
        _slider.maxValue = totalExp;
    }
    public void UpdateLevel(int level)
    {
        levelText.text = Convert.ToString(level);
    }
}
