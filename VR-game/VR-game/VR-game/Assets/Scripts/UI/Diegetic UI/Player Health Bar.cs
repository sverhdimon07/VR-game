using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image livesCounterBar1;
    [SerializeField] private Image livesCounterBar2;
    [SerializeField] private Image livesCounterBar3;
    private float lastLivesCount = 3f;
    public void RefreshUI()
    {
        healthBar.fillAmount = (PlayerHealthSystem.health) / 100;
        if (lastLivesCount != PlayerHealthSystem.currentLivesCount)
        {
            LivesCounterChecking();
            lastLivesCount = PlayerHealthSystem.currentLivesCount;
        }
    }
    private void LivesCounterChecking()
    {
        if (PlayerHealthSystem.currentLivesCount == 3f)
        {
            return;
        }
        if (PlayerHealthSystem.currentLivesCount == 2f)
        {
            livesCounterBar3.fillAmount = 0;
            return;
        }
        if (PlayerHealthSystem.currentLivesCount == 1f)
        {
            livesCounterBar2.fillAmount = 0;
            return;
        }
        if (PlayerHealthSystem.currentLivesCount == 0f)
        {
            livesCounterBar1.fillAmount = 0;
            return;
        }
    }
}