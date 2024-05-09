using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLimbAction : MonoBehaviour
{
    private const string SWORD_TAG = "PlayerSword";

    [SerializeField] private float power;
    private void OnTriggerEnter(Collider collider)
    {
        if (BossHealthSystem.currentLivesCount == 0f)
        {
            if (!collider.CompareTag(SWORD_TAG))
            {
                return;
            }
            gameObject.AddComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * power);
        }
    }
}