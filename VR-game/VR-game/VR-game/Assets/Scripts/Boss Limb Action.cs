using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLimbAction : MonoBehaviour
{
    private const string WEAPON_TAG = "Weapon";

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float power;
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag(WEAPON_TAG))
        {
            return;
        }
        rigidbody.isKinematic = false;
        rigidbody.AddForce(Vector3.up * power);
    }
}
