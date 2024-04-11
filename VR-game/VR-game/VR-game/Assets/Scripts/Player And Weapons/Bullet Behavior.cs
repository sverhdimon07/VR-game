using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private const string BOSS_TAG = "Boss";

    [SerializeField] private GameObject obj;//����������� �������� gameObject ������ �����
    public void Update()
    {

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag(BOSS_TAG))
        {
            return;
        }
        Destroy(obj);
    }
}