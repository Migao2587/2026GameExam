using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text hpText;
    public Slider ExpSlider;
    public void UpdateHealthUI(float maxHp, float hp)
    { 
        healthSlider.maxValue = maxHp;
        healthSlider.value = hp;
        healthSlider.minValue = 0;
        hpText.text = $"{hp}/{maxHp}";
    }
    public void UpdateExp(float expGap, float exp)
    { 
        ExpSlider.value = exp;
        ExpSlider.maxValue = expGap;
        ExpSlider.minValue = 0;
    }
}
