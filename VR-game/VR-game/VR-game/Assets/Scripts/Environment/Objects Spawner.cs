using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawningObject;
    [SerializeField] private GameObject spawningObjectRotationPivot;

    private Vector3 spawnPoint;
    private void Start()
    {
        spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        InvokeRepeating("ObjectCreation", 0f, 5f);
    }
    private void ObjectCreation()
    {
        GameObject objectCopy = Instantiate(spawningObject, spawnPoint, spawningObjectRotationPivot.transform.rotation);
        objectCopy.GetComponent<ConstantForce>().relativeForce = new Vector3(0, -2000, 0);
        Destroy(objectCopy, 10f);
    }
}