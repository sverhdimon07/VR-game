using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnSpaceShipScript : MonoBehaviour
{
    [SerializeField] private GameObject spaceShip;
    private Vector3 spawnPoint;
    [SerializeField] private float spawnDelayTime;
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
            GameObject shipCopy = Instantiate(spaceShip, spawnPoint, transform.rotation);
            shipCopy.GetComponent<ConstantForce>().relativeForce = new Vector3(0,0,-1000);
            Destroy(shipCopy, 20.0f);
        }
    }
}