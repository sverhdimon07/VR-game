using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButtonsController : MonoBehaviour
{
    [SerializeField] private bool rightButtonA;
    private void Update()
    {
        rightButtonA = OVRInput.Get(OVRInput.Button.One);
    }
}