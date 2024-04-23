using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnSpaceShipScript : MonoBehaviour
{
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject shipRotationPivot;
    [SerializeField] private float spawnDelayTime;
    private Vector3 spawnPoint;
    private float nextSpawnTime = 0.0f;
    void Start()
    {

    }
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnDelayTime;
            spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject shipCopy = Instantiate(spaceShip, spawnPoint, shipRotationPivot.transform.rotation);
            shipCopy.GetComponent<ConstantForce>().relativeForce = new Vector3(0,-2000,0);
            Destroy(shipCopy, 10.0f);
        }
    }
}