using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    private const string BLOCK = "Block";
    private const string WALKING = "Walking";
    private const string RIGHT_ATTACK = "RightAttack";

    //private static readonly int RIGHT_ATTACK = Animator.StringToHash("RightAttack");
    //private static readonly int WALKING = Animator.StringToHash("Walking");

    [SerializeField] private Animator animator;
    public void PlayRightAttack()
    {
        animator.SetTrigger(RIGHT_ATTACK);
    }
    public void PlayLeftAttack() 
    {
        
    }
    public void ControlWalkingAnimation(bool condition)
    {
        animator.SetBool(WALKING, condition);
    }
    public void ControlBlockAnimation(bool condition)
    {
        animator.SetBool(BLOCK, condition);
    }
}