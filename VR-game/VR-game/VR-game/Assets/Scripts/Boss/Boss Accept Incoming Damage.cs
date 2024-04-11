using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAcceptIncomingDamage : MonoBehaviour
{
    private const string SWORD_TAG = "Sword";
    private const string BULLET_TAG = "Bullet";

    private float swordDamage = 10f;
    private float gunBulletDamage = 5f;

    [SerializeField] private GameObject obj; //�������� � �� ������ �����
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material whiteMaterial;
    private void Update() //�������� ����� � ������ ��������� �� ��� �������
    {
        
    }
    private void AcceptSwordDamage()
    {
        if (BossHeathSystem.Health <= 0)
        {
            obj.GetComponent<SkinnedMeshRenderer>().material = yellowMaterial;//������� ����� ������, � ����� ����� �������� ��������� �����������
            return;//�������� � �������� �������� ������
        }
        BossHeathSystem.Health -= swordDamage;
        Debug.Log(BossHeathSystem.Health);
    }
    private void AcceptGunBulletDamage()
    {
        if (BossHeathSystem.Health <= 0)
        {
            obj.GetComponent<SkinnedMeshRenderer>().material = yellowMaterial;//������� ����� ������, � ����� ����� �������� ��������� �����������
            return;//�������� � �������� �������� ������
        }
        BossHeathSystem.Health -= gunBulletDamage;
        Debug.Log(BossHeathSystem.Health);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if ((!collider.CompareTag(SWORD_TAG)) && (!collider.CompareTag(BULLET_TAG)))
        {
            return;
        }
        if (collider.CompareTag(SWORD_TAG))
        {
            AcceptSwordDamage();
            if (BossHeathSystem.Health <= 0)
            {
                return;
            }
            obj.GetComponent<SkinnedMeshRenderer>().material = redMaterial;
            Invoke("BodyTurnWhite", 0.25f); //������ ��� ��� ��������
        }
        if (collider.CompareTag(BULLET_TAG))
        {
            AcceptGunBulletDamage();
            if (BossHeathSystem.Health <= 0)
            {
                return;
            }
            obj.GetComponent<SkinnedMeshRenderer>().material = redMaterial;
            Invoke("BodyTurnWhite", 0.25f); //������ ��� ��� ��������
        }
    }
    private void BodyTurnWhite()
    {
        obj.GetComponent<SkinnedMeshRenderer>().material = whiteMaterial;
    }
}