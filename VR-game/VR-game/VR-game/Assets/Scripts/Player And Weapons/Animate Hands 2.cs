using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AnimateHands2 : MonoBehaviour
{
    private const string SWORD_TAG = "PlayerSword";
    private const string GUN_TAG = "PlayerGun";

    private const string GRIP = "Grip";
    private const string TRIGGER = "Trigger";

    [SerializeField] private Animator animator;
    [SerializeField] private InputActionProperty gripAnimationAction;
    [SerializeField] private InputActionProperty triggerAnimationAction;

    [SerializeField] private UnityEvent weaponTaked;
    [SerializeField] private UnityEvent weaponThrew;

    private bool delay = false;
    private bool wasLessOne = false;
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag(SWORD_TAG) && !collider.CompareTag(GUN_TAG))
        {
            return;
        }
        if (collider.CompareTag(SWORD_TAG) || collider.CompareTag(GUN_TAG))
        {
            weaponTaked.Invoke();
        }
    }
    private void OnTriggerStay(Collider collider)
    {
        if (!collider.CompareTag(SWORD_TAG) && !collider.CompareTag(GUN_TAG))
        {
            return;
        }
        if (collider.CompareTag(SWORD_TAG) || collider.CompareTag(GUN_TAG))
        {
            float gripValue = gripAnimationAction.action.ReadValue<float>();
            if (gripValue < 1f)
            {
                wasLessOne = true;
            }
            if ((gripValue == 1f) && (animator.GetFloat(GRIP) == 0f) && (delay == false))
            {
                wasLessOne = false;
                animator.SetFloat(GRIP, 1f);
            }
            if ((gripValue == 1f) && (animator.GetFloat(GRIP) == 1f) && (wasLessOne == true))
            {
                weaponTaked.Invoke();
                animator.SetFloat(GRIP, 0f);
                delay = true;
                Invoke(nameof(MakingDelayFalse), 0.1f);
            }

            float triggerValue = triggerAnimationAction.action.ReadValue<float>();
            animator.SetFloat(TRIGGER, triggerValue);
        }
    }
    /*
    private void OnTriggerExit(Collider collider)
    {
        if (!collider.CompareTag(SWORD_TAG) && !collider.CompareTag(GUN_TAG))
        {
            return;
        }
        if (collider.CompareTag(SWORD_TAG) || collider.CompareTag(GUN_TAG))
        {
            weaponThrew.Invoke();
        }
    }*/
    private void MakingDelayFalse()
    {
        delay = false;
    }
}