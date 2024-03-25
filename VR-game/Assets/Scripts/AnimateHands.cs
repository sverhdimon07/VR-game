using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHands : MonoBehaviour
{
    private const string GRIP = "Grip";
    private const string TRIGGER = "Trigger";

    [SerializeField] private Animator animator;
    [SerializeField] private InputActionProperty gripAnimationAction;
    [SerializeField] private InputActionProperty triggerAnimationAction;
    private void Update()
    {
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        float triggerValue = triggerAnimationAction.action.ReadValue<float>();

        animator.SetFloat(GRIP, gripValue);
        animator.SetFloat(TRIGGER, triggerValue);
    }
}