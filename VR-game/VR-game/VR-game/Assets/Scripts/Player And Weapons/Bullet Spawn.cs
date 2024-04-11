using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float BulletSpawnDelay;
    private float newTimeBulletSpawn = 0.0f;

    [SerializeField] private InputActionProperty gripClick;
    [SerializeField] private InputActionProperty triggerClick;
    private void Update()
    {
        if ((gripClick.action.ReadValue<float>() > 0) && (triggerClick.action.ReadValue<float>() > 0))
        {
            BulletCreation();
        }
    }
    private void BulletCreation()
    {
        if (Time.time < newTimeBulletSpawn)
        {
            return;
        }
        newTimeBulletSpawn = Time.time + BulletSpawnDelay;
        GameObject bulletCopy = Instantiate(obj, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletCopy.GetComponent<ConstantForce>().relativeForce = new Vector3(0,0,100);
        Destroy(bulletCopy, 3.0f);
    }
}