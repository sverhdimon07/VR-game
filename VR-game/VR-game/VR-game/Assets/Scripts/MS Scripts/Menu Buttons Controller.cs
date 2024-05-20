using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuButtonsController : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerActivated;

    [SerializeField] InputActionProperty triggerAction;
    private void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();

        if (triggerValue > 0f) 
        {
            triggerActivated.Invoke();
        }
    }
}