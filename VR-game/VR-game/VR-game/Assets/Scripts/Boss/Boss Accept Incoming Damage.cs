using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAcceptIncomingDamage : MonoBehaviour
{
    private const string SWORD_TAG = "Sword";
    private const string BULLET_TAG = "Bullet";

    private float swordDamage = 10f;
    private float gunBulletDamage = 1f;

    [SerializeField] private GameObject bodyMesh;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material whiteMaterial;

    private bool bossWasDamaged = false;
    private bool stagger = false;
    private void Update() //потом удалить, где он не требуется (для гигиены кода)
    {

    }
    private void AcceptSwordDamage()
    {
        if (BossHealthSystem.health <= 0)
        {
            bodyMesh.GetComponent<SkinnedMeshRenderer>().material = yellowMaterial;//сделать врага желтым, и после этого включить отрубание конечностей
            return;//передать в аниматор анимацию смерти
        }
        BossHealthSystem.health -= swordDamage;
        Debug.Log(BossHealthSystem.health);
        if (BossHealthSystem.health <= 0)
        {
            bodyMesh.GetComponent<SkinnedMeshRenderer>().material = yellowMaterial;//сделать врага желтым, и после этого включить отрубание конечностей
            return;//передать в аниматор анимацию смерти
        }
    }
    private void AcceptGunBulletDamage()
    {
        if (BossHealthSystem.health <= 0)
        {
            bodyMesh.GetComponent<SkinnedMeshRenderer>().material = yellowMaterial;//сделать врага желтым, и после этого включить отрубание конечностей
            return;//передать в аниматор анимацию смерти
        }
        BossHealthSystem.health -= gunBulletDamage;
        Debug.Log(BossHealthSystem.health);
        if (BossHealthSystem.health <= 0)
        {
            bodyMesh.GetComponent<SkinnedMeshRenderer>().material = yellowMaterial;//сделать врага желтым, и после этого включить отрубание конечностей
            return;//передать в аниматор анимацию смерти
        }
    }
    private void OnTriggerEnter(Collider collider)
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
                        stagger = true;
                        StaggerExit();
                        return;
                    }
                    bodyMesh.GetComponent<SkinnedMeshRenderer>().material = redMaterial;
                    Invoke("BodyTurnWhite", 0.25f); //узнать как оно работает
                }
                if (collider.CompareTag(BULLET_TAG))
                {
                    AcceptGunBulletDamage();
                    if (BossHealthSystem.health <= 0)
                    {
                        stagger = true;
                        StaggerExit();
                        return;
                    }
                    bodyMesh.GetComponent<SkinnedMeshRenderer>().material = redMaterial;
                    Invoke("BodyTurnWhite", 0.25f); //узнать как оно работает
                }
            }
        }
    }
    private void BodyTurnWhite()
    {
        bodyMesh.GetComponent<SkinnedMeshRenderer>().material = whiteMaterial;
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
}