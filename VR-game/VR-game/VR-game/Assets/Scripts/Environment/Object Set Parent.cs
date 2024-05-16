using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetParent : MonoBehaviour
{
    [SerializeField] private Transform objTransform;
    [SerializeField] private Transform anotherObjTransform;
    public void SetParentToWorld()
    {
        objTransform.SetParent(null);
    }
    public void SetParentToAnotherObj()
    {
        objTransform.SetParent(anotherObjTransform);
    }
}
