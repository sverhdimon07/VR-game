using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PSBossAcceptIncomingDamage : MonoBehaviour
{
    private const string SWORD_TAG = "PlayerSword";

    private float swordDamage = 10f;
    private float staggerExitTime = 5f;

    [SerializeField] private Transform bossTransform;
    [SerializeField] private Transform pointTransform;

    [SerializeField] private SkinnedMeshRenderer bossRender;

    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material whiteMaterial;

    private bool bossWasDamagedState = false;
    public static bool staggerState = false;
    private bool bossWasKilledState = false;

    [SerializeField] private UnityEvent bossDamaged;
    [SerializeField] private UnityEvent bossDamagedBySword;
    [SerializeField] private UnityEvent secondPhaseLifeDestroyed;
    [SerializeField] private UnityEvent regenerationEnded;
    [SerializeField] private UnityEvent bossDied;
    private void AcceptSwordDamage()
    {
        BossHealthSystem.secondHealth -= swordDamage;
        bossDamaged.Invoke();
        bossDamagedBySword.Invoke();
        Debug.Log(BossHealthSystem.secondHealth);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (BossHealthSystem.counterBossLifeDestroyed == 1)
        {
            if (bossWasKilledState == false)
            {
                if (bossWasDamagedState == false)
                {
                    bossWasDamagedState = true;
                    DamageCooldown(); // �������� ������ �������� ��� ������ � ��� ��������

                    if (!collider.CompareTag(SWORD_TAG))
                    {
                        return;
                    }
                    if (collider.CompareTag(SWORD_TAG))
                    {
                        AcceptSwordDamage();
                        if ((BossHealthSystem.secondHealth <= 0) && (BossHealthSystem.counterBossLifeDestroyed == 1))
                        {
                            BossHealthSystem.counterBossLifeDestroyed += 1;

                            BossHealthSystem.currentLivesCount -= 1;

                            if (BossHealthSystem.currentLivesCount == 0f)
                            {
                                BossHealthSystem.counterBossLifeDestroyed = -1;

                                bossWasKilledState = true;
                                bossDied.Invoke();
                                return;
                            }

                            secondPhaseLifeDestroyed.Invoke();

                            BossHealthSystem.counterBossLifeDestroyed = 0;

                            ChangeBodyColor(greenMaterial);
                            staggerState = true;
                            StaggerExit(staggerExitTime);
                            return;
                        }
                        ChangeBodyColor(redMaterial);
                        Invoke(nameof(BodyTurnWhite), 0.25f);
                    }
                }
            }
        }
    }
    private void BodyTurnWhite()
    {
        ChangeBodyColor(whiteMaterial);
    }
    private void Regeneration()
    {
        BossHealthSystem.HealthRegeneration();
        BossHealthSystem.SecondHealthRegeneration();
    }
    private void MakingStaggerFalse()
    {
        staggerState = false;
    }
    private void StaggerExit(float time)
    {
        Invoke(nameof(MakingStaggerFalse), time);
        Invoke(nameof(Regeneration), time);
        Invoke(nameof(BodyTurnWhite), time);
        Invoke(nameof(InvokingRegenerationEndedEvent), time);
    }
    private void InvokingRegenerationEndedEvent()
    {
        regenerationEnded.Invoke();
    }
    private void MakingBossWasDamagedFalse()
    {
        bossWasDamagedState = false;
    }
    private void DamageCooldown()
    {
        Invoke(nameof(MakingBossWasDamagedFalse), 0.1f);
    }
    private void ChangeBodyColor(Material material)
    {
        bossRender.material = material;
    }
    public void ChangeBossPosition()
    {
        bossTransform.position = pointTransform.position;
    }
}