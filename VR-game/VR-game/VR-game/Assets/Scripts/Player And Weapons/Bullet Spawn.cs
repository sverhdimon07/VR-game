using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{
    private const string LEFTHAND_TAG = "PlayerLeftHand";
    private const string RIGHTHAND_TAG = "PlayerRightHand";

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float BulletSpawnDelay;
    private float newTimeBulletSpawn = 0.0f;
    private bool gripActivated = false;

    [SerializeField] private InputActionProperty leftGripClick;
    [SerializeField] private InputActionProperty leftTriggerClick;
    [SerializeField] private InputActionProperty rightGripClick;
    [SerializeField] private InputActionProperty rightTriggerClick;
    private void OnTriggerStay(Collider collider)
    {
        if (!collider.CompareTag(LEFTHAND_TAG) && !collider.CompareTag(RIGHTHAND_TAG))
        {
            return;
        }
        if (collider.CompareTag(LEFTHAND_TAG))
        {
            if (leftGripClick.action.ReadValue<float>() > 0)
            {
                gripActivated = true;
            }
            if ((leftTriggerClick.action.ReadValue<float>() > 0) && (gripActivated == true))
            {
                BulletCreation();
            }
        }
        if (collider.CompareTag(RIGHTHAND_TAG))
        {
            if (rightGripClick.action.ReadValue<float>() > 0)
            {
                gripActivated = true;
            }
            if ((rightTriggerClick.action.ReadValue<float>() > 0) && (gripActivated == true))
            {
                BulletCreation();
            }
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