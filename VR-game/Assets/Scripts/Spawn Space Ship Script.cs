using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnSpaceShipScript : MonoBehaviour
{
    [SerializeField] private GameObject obj;
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
            GameObject ship = Instantiate(obj, spawnPoint, transform.rotation);
            Destroy(ship, 20.0f);
        }
    }
}