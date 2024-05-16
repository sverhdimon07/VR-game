using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;

    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSource3;
    [SerializeField] private AudioSource audioSource4;

    public void PlaySound(int number)
    {
        if (number == 0)
        {
            audioSource2.loop = true;
            audioSource2.Play();
        }
        else if (number == 3)
        {
            audioSource3.Play();
        }
        else if (number == 4)
        {
            audioSource4.Play();
            Invoke(nameof(StopPlayFourthAudioSource), 7f);
        }
        else
        {
            audioSource1.PlayOneShot(sounds[number], 0.25f);
        }
    }
    private void StopPlayFourthAudioSource()
    {
        audioSource4.Stop();
    }
    public void StopPlayThirdAuidoSource()
    {
        audioSource3.Stop();
    }

}