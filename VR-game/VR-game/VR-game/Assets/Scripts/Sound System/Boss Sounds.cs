using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;

    [SerializeField] private AudioSource audioSource1;
    public void PlaySound(int number)
    {
        if (number == 7)
        {
            audioSource1.PlayOneShot(sounds[number], 0.08f);
        }
        else
        {
            audioSource1.PlayOneShot(sounds[number], 0.2f);
        }
    }
}