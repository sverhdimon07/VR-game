using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTorsoInertia : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject leftLeg;
    [SerializeField] private GameObject rightLeg;
    void Update()
    {
        if ((leftLeg.GetComponent<Rigidbody>().isKinematic == false) && (rightLeg.GetComponent<Rigidbody>().isKinematic == false))
        {
            rigidbody.isKinematic = false;
        }
    }
}
