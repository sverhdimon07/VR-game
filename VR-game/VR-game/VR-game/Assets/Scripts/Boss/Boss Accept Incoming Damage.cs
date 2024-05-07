using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BossAcceptIncomingDamage : MonoBehaviour
{
    private const string SWORD_TAG = "Sword";
    private const string BULLET_TAG = "Bullet";

    private float swordDamage = 10f;
    private float gunBulletDamage = 0.5f;
    private float staggerExitTime = 3f;

    [SerializeField] private MeshRenderer headRender;
    [SerializeField] private MeshRenderer torsoRender;
    [SerializeField] private MeshRenderer leftArmRender;
    [SerializeField] private MeshRenderer leftLegRender;
    [SerializeField] private MeshRenderer rightArmRender;
    [SerializeField] private MeshRenderer rightLegRender;

    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material whiteMaterial;

    private bool bossWasDamagedState = false;
    private bool staggerState = false;
    private bool bossWasKilledState = false;

    [SerializeField] private UnityEvent bossDamaged;//посмотреть на синтаксис
    [SerializeField] private UnityEvent LifeDestroyed;
    [SerializeField] private UnityEvent regenerationEnded;
    private void AcceptSwordDamage()
    {
        BossHealthSystem.health -= swordDamage;
        bossDamaged.Invoke();//разобраться с вопросительным знаком
        Debug.Log(BossHealthSystem.health);
        if (BossHealthSystem.health <= 0)
        {
            ChangeBodyColor(yellowMaterial);//передать в аниматор анимацию смерти
        }
    }
    private void AcceptGunBulletDamage()
    {
        BossHealthSystem.health -= gunBulletDamage;
        bossDamaged.Invoke();
        Debug.Log(BossHealthSystem.health);
        if (BossHealthSystem.health <= 0)
        {
            ChangeBodyColor(yellowMaterial);//передать в аниматор анимацию смерти
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (bossWasKilledState == false)
        {
            if (bossWasDamagedState == false)
            {
                bossWasDamagedState = true;
                DamageCooldown(); // добавить разную задержку для катаны и для бластера
                if (staggerState == false)
                {
                    if (!collider.CompareTag(SWORD_TAG) && !collider.CompareTag(BULLET_TAG))
                    {
                        return;
                    }
                    if (collider.CompareTag(SWORD_TAG))
                    {
                        AcceptSwordDamage();
                        if (BossHealthSystem.health <= 0)
                        {
                            BossHealthSystem.currentLivesCount -= 1;
                            LifeDestroyed.Invoke();
                            if (BossHealthSystem.currentLivesCount == 0f)
                            {
                                ChangeBodyColor(greenMaterial);
                                bossWasKilledState = true;
                                return;
                            }
                            staggerState = true;
                            StaggerExit(staggerExitTime);
                            return;
                        }
                        ChangeBodyColor(redMaterial);
                        Invoke(nameof(BodyTurnWhite), 0.25f); //узнать как оно работает
                    }
                    if (collider.CompareTag(BULLET_TAG))
                    {
                        AcceptGunBulletDamage();
                        if (BossHealthSystem.health <= 0)
                        {
                            BossHealthSystem.currentLivesCount -= 1;
                            LifeDestroyed.Invoke();
                            if (BossHealthSystem.currentLivesCount == 0f)
                            {
                                ChangeBodyColor(greenMaterial);
                                bossWasKilledState = true;
                                return;
                            }
                            staggerState = true;
                            StaggerExit(staggerExitTime);
                            return;
                        }
                        ChangeBodyColor(redMaterial);
                        Invoke(nameof(BodyTurnWhite), 0.25f); //узнать как оно работает
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
    private void MakingBossWasDamagedFalse()
    {
        bossWasDamagedState = false;
    }
    private void DamageCooldown()
    {
        Invoke(nameof(MakingBossWasDamagedFalse), 0.1f);
    }
    private void InvokingRegenerationEndedEvent()
    {
        regenerationEnded.Invoke();
    }
    private void ChangeBodyColor(Material material)
    {
        headRender.material = material;
        torsoRender.material = material;
        leftArmRender.material = material;
        leftLegRender.material = material;
        rightArmRender.material = material;
        rightLegRender.material = material;
    }
}