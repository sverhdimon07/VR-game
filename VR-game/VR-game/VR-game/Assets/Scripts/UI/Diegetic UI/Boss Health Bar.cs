using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image livesCounterBar1;
    [SerializeField] private Image livesCounterBar2;
    [SerializeField] private Image livesCounterBar3;
    private float lastLivesCount = 3f;
    public void RefreshUI()
    {
        healthBar.fillAmount = (BossHealthSystem.health)/100;
        if (lastLivesCount != BossHealthSystem.currentLivesCount)
        {
            LivesCounterChecking();
            lastLivesCount = BossHealthSystem.currentLivesCount;
        }
    }
    private void LivesCounterChecking()
    {
        if (BossHealthSystem.currentLivesCount == 3f)
        {
            return;
        }
        if (BossHealthSystem.currentLivesCount == 2f)
        {
            livesCounterBar3.fillAmount = 0;
            return;
        }
        if (BossHealthSystem.currentLivesCount == 1f)
        {
            livesCounterBar2.fillAmount = 0;
            return;
        }
        if (BossHealthSystem.currentLivesCount == 0f)
        {
            livesCounterBar1.fillAmount = 0;
            return;
        }
    }
}