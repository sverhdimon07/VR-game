using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TableSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource1;
    private void Start()
    {
        audioSource1.loop = true;
        audioSource1.Play();
    }
}