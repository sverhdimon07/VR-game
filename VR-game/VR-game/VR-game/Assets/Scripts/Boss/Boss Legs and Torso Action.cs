using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLegsandTorsoAction : MonoBehaviour
{
    private const string SWORD_TAG = "Sword";

    [SerializeField] private GameObject torso;
    [SerializeField] private GameObject leftLeg;
    [SerializeField] private GameObject rightLeg;
    [SerializeField] private float power;
    public void Update()
    {

    }
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
            if ((leftLeg.GetComponent<Rigidbody>().isKinematic == false) && (rightLeg.GetComponent<Rigidbody>().isKinematic == false))
            {
                torso.AddComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}