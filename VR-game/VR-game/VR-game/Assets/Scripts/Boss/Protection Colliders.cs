using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProtectionColliders : MonoBehaviour
{
    private const string SWORD_TAG = "PlayerSword";

    [SerializeField] private Collider shieldCollider;

    [SerializeField] private UnityEvent shieldActivated;
    private void Start()
    {
        DisableCollider();
    }
    public void DisableCollider()
    {
        shieldCollider.enabled = false;
    }
    public void EnableCollider()
    {
        shieldCollider.enabled = true;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag(SWORD_TAG))
        {
            return;
        }
        shieldActivated.Invoke();
    }
}