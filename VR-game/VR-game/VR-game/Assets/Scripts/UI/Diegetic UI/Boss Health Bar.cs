using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    private void Update()
    {
        bar.fillAmount = (BossHealthSystem.health)/100;
    }
}