using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAcceptIncomingDamage : MonoBehaviour
{
    private const string SWORD_TAG = "Sword";
    private const string BULLET_TAG = "Bullet";

    private float swordDamage = 10f;
    private float gunBulletDamage = 1f;

    [SerializeField] private GameObject headRender;
    [SerializeField] private GameObject torsoRender;
    [SerializeField] private GameObject leftArmRender;
    [SerializeField] private GameObject leftLegRender;
    [SerializeField] private GameObject rightArmRender;
    [SerializeField] private GameObject rightLegRender;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material whiteMaterial;

    private bool bossWasDamaged = false;
    private bool stagger = false;
    private bool bossWasKilled = false;
    private void Update() //����� �������, ��� �� �� ��������� (��� ������� ����)
    {

    }
    private void AcceptSwordDamage()
    {
        BossHealthSystem.health -= swordDamage;
        Debug.Log(BossHealthSystem.health);
        if (BossHealthSystem.health <= 0)
        {
            ChangeBodyColor(yellowMaterial);//�������� � �������� �������� ������
        }
    }
    private void AcceptGunBulletDamage()
    {
        BossHealthSystem.health -= gunBulletDamage;
        Debug.Log(BossHealthSystem.health);
        if (BossHealthSystem.health <= 0)
        {
            ChangeBodyColor(yellowMaterial);//�������� � �������� �������� ������
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (bossWasKilled == false)
        {
            if (bossWasDamaged == false)
            {
                bossWasDamaged = true;
                DamageCooldown();
                if (stagger == false)
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
                            if (BossHealthSystem.currentLivesCount == 0f)
                            {
                                ChangeBodyColor(greenMaterial);
                                bossWasKilled = true;
                                return;
                            }
                            stagger = true;
                            StaggerExit();
                            return;
                        }
                        ChangeBodyColor(redMaterial);
                        Invoke("BodyTurnWhite", 0.25f); //������ ��� ��� ��������
                    }
                    if (collider.CompareTag(BULLET_TAG))
                    {
                        AcceptGunBulletDamage();
                        if (BossHealthSystem.health <= 0)
                        {
                            BossHealthSystem.currentLivesCount -= 1;
                            if (BossHealthSystem.currentLivesCount == 0f)
                            {
                                ChangeBodyColor(greenMaterial);
                                bossWasKilled = true;
                                return;
                            }
                            stagger = true;
                            StaggerExit();
                            return;
                        }
                        ChangeBodyColor(redMaterial);
                        Invoke("BodyTurnWhite", 0.25f); //������ ��� ��� ��������
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
        stagger = false;
    }
    private void StaggerExit()
    {
        Invoke("MakingStaggerFalse", 3f);
        Invoke("Regeneration", 3f);
        Invoke("BodyTurnWhite", 3f);
    }
    private void MakingBossWasDamagedFalse()
    {
        bossWasDamaged = false;
    }
    private void DamageCooldown()
    {
        Invoke("MakingBossWasDamagedFalse", 0.1f);
    }
    private void ChangeBodyColor(Material material)
    {
        headRender.GetComponent<MeshRenderer>().material = material;
        torsoRender.GetComponent<MeshRenderer>().material = material;
        leftArmRender.GetComponent<MeshRenderer>().material = material;
        leftLegRender.GetComponent<MeshRenderer>().material = material;
        rightArmRender.GetComponent<MeshRenderer>().material = material;
        rightLegRender.GetComponent<MeshRenderer>().material = material;
    }
}