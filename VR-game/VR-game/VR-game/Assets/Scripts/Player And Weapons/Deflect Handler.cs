using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeflectHandler : MonoBehaviour
{
    private const string BOSS_SWORD_TAG = "BossSword";

    private bool handlerActivated = false;

    [SerializeField] private UnityEvent playerBlockActivated;
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag(BOSS_SWORD_TAG))
        {
            return;
        }
        if (handlerActivated == true)
        {
            playerBlockActivated.Invoke();
        }
    }
    public void MakeHandlerAcivatedTrue()
    {
        handlerActivated = true;
    }
    public void MakeHandlerAcivatedFalse()
    {
        handlerActivated = false;
    }
}