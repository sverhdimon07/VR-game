using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner2 : MonoBehaviour
{
    [SerializeField] private GameObject spawningObject;
    [SerializeField] private GameObject spawningObjectRotationPivot;

    private Vector3 spawnPoint;
    private void Awake()
    {
        enabled = false;
    }
    private void Start()
    {
        spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (int i = 0; i < 4; i++)
        {
            Invoke(nameof(ObjectCreation), (i + 2f) / 1.5f);
        }
    }
    public void ObjectCreation()
    {
        GameObject objectCopy = Instantiate(spawningObject, spawnPoint, spawningObjectRotationPivot.transform.rotation);
        objectCopy.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, -5000);
        Destroy(objectCopy, 10f);
    }
}