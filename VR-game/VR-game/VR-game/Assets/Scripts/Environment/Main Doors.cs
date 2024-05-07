using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoors : MonoBehaviour
{
    private const string PLAYER_TAG = "PlayerBox";

    [SerializeField] private Transform door1;
    [SerializeField] private Transform door2;

    private bool enterState = false;
    private bool exitState = true;

    private Vector3 destination1;
    private Vector3 destination2;
    private Vector3 destination3;
    private Vector3 destination4;
    private void Start()
    {
        destination1 = door1.position + new Vector3(3f, 0, 0);
        destination2 = door2.position - new Vector3(3f, 0, 0);
        destination3 = door1.position;
        destination4 = door2.position;
        enabled = false;
    }
    private void Update()
    {
        if (enterState == true)
        {
            OpenDoors(0.1f);
        }
        else if (exitState == true)
        {
            CloseDoors(0.1f);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag(PLAYER_TAG))
        {
            return;
        }
        enterState = true;
        exitState = false;
        enabled = true;
    }
    private void OnTriggerExit(Collider collider)
    {
        if (!collider.CompareTag(PLAYER_TAG))
        {
            return;
        }
        enterState = false;
        exitState = true;
    }
    private void OpenDoors(float speed)
    {
        door1.position = Vector3.Lerp(door1.position, destination1, speed);
        door2.position = Vector3.Lerp(door2.position, destination2, speed);
    }
    private void CloseDoors(float speed)
    {
        door1.position = Vector3.Lerp(door1.position, destination3, speed);
        door2.position = Vector3.Lerp(door2.position, destination4, speed);
    }
}