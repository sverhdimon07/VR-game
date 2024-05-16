using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public class BossAI : MonoBehaviour
{
    [SerializeField] private BossAnimator bossAnimator;

    [SerializeField] private Transform bossTransform;
    [SerializeField] private Transform playerTracker;
    [SerializeField] private Transform centerPoint;

    [SerializeField] private AIDestinationSetter destinationSetter;

    [SerializeField] private Transform roamTarget;

    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    private Vector3 roamPosition;

    //private Player player => FindObjectOfType<Player>();
    private Player player;

    private bool cooldownActivated = false;

    private float enemyAtackRange = 3f;

    private bool roamingState = false;
    private bool followingState = false;
    private bool notFollowingState = false;
    private bool deathState = false;

    private bool protectionState = false;
    private bool atackState = false;
    private bool openingState = false;

    [SerializeField] private UnityEvent protectionActivated;
    [SerializeField] private UnityEvent openingActivated;
    private void Start()
    {
        player = FindObjectOfType<Player>();

        roamingState = true;
        followingState = false;

        roamPosition = point1.position;

        bossAnimator.ControlWalkingAnimation(true);
    }
    private void Update()
    {
        if (roamingState == true)
        {
            RoamingStateLogic();
            return;
        }
        else if (followingState == true)
        {
            bossTransform.LookAt(playerTracker);
            FollowingStateLogic();

            return;
        }
        else if (notFollowingState == true)
        {
            bossTransform.LookAt(playerTracker);

            if ((protectionState == true) && (openingState == false))
            {
                protectionActivated.Invoke();
            }
            else if ((protectionState == false) && (openingState == true))
            {
                openingActivated.Invoke();
            }
            return;
        }
        else if (deathState == true)
        {
            return;
        }
    }
        //добавить состояния стаггера (мб связать с BossAID), смерти, кастинга спеллов в точке (подумать про место реализации состояния защиты босса и состояния локального стаггера)
        //глобальный (потеря жизни) и локальный (потеря концентрации) стаггер
        //скрипт попытки нанесения урона боссом, сцены
        //добавление состояние спелкастов босса

        //не забыть - запретить выпускать рейкаст из пушки во время 2х фаз
        //сделать эффект того, что пистолет сломан во 2х фазах

        //продумать логику смерти игрока

        //что то делать при ликвидации жизни игрока
        //продумать систему, когда босс восстанавливает жизнь, когда игрок теряет жизнь
    private void RoamingStateLogic()
    {
        roamTarget.position = roamPosition;
        if (Vector3.Distance(gameObject.transform.position, roamPosition) <= 1f)
        {
            if (roamPosition == point1.position)
            {
                roamPosition = point2.position;
            }
            else if (roamPosition == point2.position)
            {
                roamPosition = point1.position;
            }
        }
        destinationSetter.target = roamTarget;
    }
    private void FollowingStateLogic()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= enemyAtackRange)
        {
            destinationSetter.target = null;

            ProtectionState();

            if (cooldownActivated == false)
            {
                TryAttackPlayer();
                //Cooldown(5f);
            }

        }
        else
        {
            OpeningState();

            destinationSetter.target = player.transform;
        }
    }

    private void Cooldown(float timeEnding)
    {
        float timeCounter = 0;
        timeCounter += Time.deltaTime;
        cooldownActivated = true;
        if (timeCounter >= timeEnding)
        {
            cooldownActivated = false;
        }    
    }
    private void TryAttackPlayer()
    {
        bossAnimator.ControlWalkingAnimation(false);

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= enemyAtackRange)
        {
            bossAnimator.PlayRightAttack();
        }
    }
    private void ProtectionState()
    {
        protectionState = true;
        openingState = false;

        bossAnimator.ControlWalkingAnimation(false);
        bossAnimator.ControlBlockAnimation(true);

        protectionActivated.Invoke();
    }
    private void OpeningState()
    {
        protectionState = false;
        openingState = true;

        bossAnimator.ControlBlockAnimation(false);
        bossAnimator.ControlWalkingAnimation(true);

        openingActivated.Invoke();
    }
    public void MakingNotFollowingStateTrue()
    {
        roamingState = false;
        followingState = false;
        notFollowingState = true;
    }
    public void MakingNotFollowingStateFalse()
    {
        roamingState = false;
        followingState = true;
        notFollowingState = false;
    }
    public void TryFindPlayer()
    {
        roamingState = false;
        followingState = true;
    }
    public void ActivateDeathState()
    {
        roamingState = false;
        followingState = false;
        notFollowingState = false;
        protectionState = false;
        notFollowingState = false;

        deathState = true;
    }
}