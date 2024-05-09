using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter destinationSetter;

    //[SerializeField] private BossAtack;

    [SerializeField] private Transform roamTarget;

    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    private Vector3 roamPosition;

    private Player player;

    private bool roamingState = false;
    private bool followingState = false;

    private float enemyAtackRange = 3f;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        roamingState = true;
        followingState = false;
        roamPosition = point1.position;
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
            FollowingStateLogic();
            return;
        }
        //добавить состояния стаггера (мб связать с BossAID), смерти, кастинга спеллов в точке (подумать про место реализации состояния защиты босса и состояниялокального стаггера)
        //глобальный (потеря жизни) и локальный (потеря концентрации) стаггер
        //изучить enum
        //скрипт попытки нанесения урона боссом, скрипт PlayerAcceptIncomingDamage, смерть игрока, сцены
        //добавление состояние спелкастов босса
    }
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
            //TryAttackPlayer();
        }
        else
        {
            destinationSetter.target = player.transform; //написать изменение положения этого объекта при помощи Lerp
        }
    }
    public void TryFindPlayer()
    {
        roamingState = false;
        followingState = true;
    }
    private void TryAttackPlayer()
    {
        //сделать анимацию
    }
}