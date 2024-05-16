using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PFBossAcceptIncomingDamage : MonoBehaviour
{
    private const string SWORD_TAG = "PlayerSword";
    private const string BULLET_TAG = "Bullet";

    private float swordDamage = 10f;
    private float gunBulletDamage = 0.5f;

    [SerializeField] private SkinnedMeshRenderer bossRender;

    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material whiteMaterial;

    private bool bossWasDamagedState = false;

    [SerializeField] private UnityEvent bossDamaged;
    [SerializeField] private UnityEvent bossDamagedBySword;
    [SerializeField] private UnityEvent firstPhaseLifeDestroyed;
    private void AcceptSwordDamage()
    {
        BossHealthSystem.health -= swordDamage;
        bossDamaged.Invoke();
        bossDamagedBySword.Invoke();
        Debug.Log(BossHealthSystem.health);
    }
    private void AcceptGunBulletDamage()
    {
        BossHealthSystem.health -= gunBulletDamage;
        bossDamaged.Invoke();
        Debug.Log(BossHealthSystem.health);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if ((BossHealthSystem.counterBossLifeDestroyed == 0) && (PSBossAcceptIncomingDamage.staggerState == false))
        {
            if (bossWasDamagedState == false)
            {
                bossWasDamagedState = true;
                DamageCooldown(); //добавить разную задержку для катаны и для бластера

                if (!collider.CompareTag(SWORD_TAG) && !collider.CompareTag(BULLET_TAG))
                {
                    return;
                }
                if (collider.CompareTag(SWORD_TAG))
                {
                    AcceptSwordDamage();
                    if ((BossHealthSystem.health <= 0) && (BossHealthSystem.counterBossLifeDestroyed == 0))
                    {
                        BossHealthSystem.counterBossLifeDestroyed += 1;
                        firstPhaseLifeDestroyed.Invoke();
                        //AddStagger(); - мб сделать стаггер перед началом 2х фаз
                        return;
                    }
                    ChangeBodyColor(redMaterial);
                    Invoke(nameof(BodyTurnWhite), 0.25f);
                }
                if (collider.CompareTag(BULLET_TAG))
                {
                    AcceptGunBulletDamage();
                    if ((BossHealthSystem.health <= 0) && (BossHealthSystem.counterBossLifeDestroyed == 0))
                    {
                        BossHealthSystem.counterBossLifeDestroyed += 1;
                        firstPhaseLifeDestroyed.Invoke();
                        return;
                    }
                    ChangeBodyColor(redMaterial);
                    Invoke(nameof(BodyTurnWhite), 0.25f);
                }
            }
        }
    }
    private void AddStagger()
    {
        PSBossAcceptIncomingDamage.staggerState = true;
        ChangeBodyColor(yellowMaterial);
        Invoke(nameof(BodyTurnWhite), 1f);
    }
    private void BodyTurnWhite()
    {
        ChangeBodyColor(whiteMaterial);
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
}