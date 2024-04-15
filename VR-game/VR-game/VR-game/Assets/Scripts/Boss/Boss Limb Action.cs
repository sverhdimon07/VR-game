using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLimbAction : MonoBehaviour
{
    private const string SWORD_TAG = "Sword";
    private const string BULLET_TAG = "Bullet";

    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private float power;
    public void Update()
    {

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (gameObject.GetComponent<BossLimbAction>().enabled == false)
        {
            return;
        }
        if ((!collider.CompareTag(SWORD_TAG)) || (!collider.CompareTag(BULLET_TAG)))
        {
            return;
        }
        rigidbody.isKinematic = false;
        rigidbody.AddForce(Vector3.up * power);
    }
}