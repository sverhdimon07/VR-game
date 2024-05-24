using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossSwordHandler : MonoBehaviour
{
    [SerializeField] private Collider swordCollider;

    [SerializeField] private UnityEvent hitHandlerDeactivated;
    private void Start()
    {
        DisableTriggerCollider();
    }
    public void EnableTriggerCollider()
    {
        swordCollider.enabled = true;
    }
    public void DisableTriggerCollider()
    {
        swordCollider.enabled = false;

        hitHandlerDeactivated.Invoke();
    }
    public void DisableHitHandler()
    {
        hitHandlerDeactivated.Invoke();
    }
}
