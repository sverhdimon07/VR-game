using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{
    private const string HAND_TAG = "PlayerHands";

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float BulletSpawnDelay;
    private float newTimeBulletSpawn = 0.0f;
    private bool gripActivated = false;

    [SerializeField] private InputActionProperty gripClick;
    [SerializeField] private InputActionProperty triggerClick;
    private void OnTriggerStay(Collider collider)
    {
        if (!collider.CompareTag(HAND_TAG))
        {
            return;
        }
        if (gripClick.action.ReadValue<float>() > 0)
        {
            gripActivated = true;
        }
        if ((triggerClick.action.ReadValue<float>() > 0) && (gripActivated == true))
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
        GameObject bulletCopy = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletCopy.GetComponent<ConstantForce>().relativeForce = new Vector3(0,0,100);
        Destroy(bulletCopy, 3.0f);
    }
}