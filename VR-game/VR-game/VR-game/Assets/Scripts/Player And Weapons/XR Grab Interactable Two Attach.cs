using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    private const string LEFTHAND_TAG = "PlayerLeftHand";
    private const string RIGHTHAND_TAG = "PlayerRightHand";

    [SerializeField] private Transform leftAttachTransform;
    [SerializeField] private Transform rightAttachTransform;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag(LEFTHAND_TAG))
        {
            attachTransform = leftAttachTransform;
        }
        if (args.interactorObject.transform.CompareTag(RIGHTHAND_TAG))
        {
            attachTransform = rightAttachTransform;
        }
        base.OnSelectEntered(args);
    }
}
