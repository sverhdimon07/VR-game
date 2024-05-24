using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPositionChange : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    [SerializeField] private Vector3 additionalDistance = new Vector3(0, 1.5f, 0);

    [SerializeField] private Transform particleTransform;
    [SerializeField] private Transform pointTransform;
    public void PlayParticles()
    {
        particleTransform.position = pointTransform.position + additionalDistance;
        particle.Play();
    }
}