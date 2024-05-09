using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAcceptIncomingDamage : MonoBehaviour
{
    private const string SWORD_TAG = "BossSword";

    private float swordDamage = 10f;
    private float staggerExitTime = 3f;

    private bool staggerState = false;
    private bool playerWasKilledState = false;

    [SerializeField] private UnityEvent playerDamaged;//посмотреть на синтаксис
    [SerializeField] private UnityEvent LifeDestroyed;
    [SerializeField] private UnityEvent regenerationEnded;
    [SerializeField] private UnityEvent playerDied;
    private void AcceptSwordDamage()
    {
        PlayerHealthSystem.health -= swordDamage;
        playerDamaged.Invoke();//разобраться с вопросительным знаком
        Debug.Log(BossHealthSystem.health);
        if (PlayerHealthSystem.health <= 0)
        {
            //сделать состояние стаггера
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (playerWasKilledState == false)
        {
            if (staggerState == false)
            {
                if (!collider.CompareTag(SWORD_TAG))
                {
                    return;
                }
                AcceptSwordDamage();
                if (PlayerHealthSystem.health <= 0)
                {
                    PlayerHealthSystem.currentLivesCount -= 1;
                    LifeDestroyed.Invoke();
                    if (PlayerHealthSystem.currentLivesCount == 0f)
                    {
                        playerWasKilledState = true;
                        playerDied.Invoke();
                        return;
                    }
                    staggerState = true;
                    StaggerExit(staggerExitTime);
                    return;
                }
            }
        }
    }
    private void Regeneration()
    {
        PlayerHealthSystem.HealthRegeneration();
    }
    private void MakingStaggerFalse()
    {
        staggerState = false;
    }
    private void StaggerExit(float time)
    {
        Invoke(nameof(MakingStaggerFalse), time);
        Invoke(nameof(Regeneration), time);
        Invoke(nameof(InvokingRegenerationEndedEvent), time);
    }
    private void InvokingRegenerationEndedEvent()
    {
        regenerationEnded.Invoke();
    }
}
