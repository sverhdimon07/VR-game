using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ArenaEventWall : MonoBehaviour
{
    private const string PLAYER_TAG = "PlayerBox";

    [SerializeField] private UnityEvent wallActivated;
    [SerializeField] private ParticleSystem dust1;
    [SerializeField] private ParticleSystem dust2;
    [SerializeField] private ParticleSystem fire1;
    [SerializeField] private ParticleSystem fire2;
    [SerializeField] private ParticleSystem fire3;
    [SerializeField] private ParticleSystem fire4;
    [SerializeField] private ParticleSystem fire5;
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag(PLAYER_TAG))
        {
            return;
        }
        wallActivated.Invoke();
    }
    private void OnTriggerExit(Collider collider)
    {
        if (!collider.CompareTag(PLAYER_TAG))
        {
            return;
        }
        TurningOnCollider();
    }
    private void TurningOnCollider()
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }
}