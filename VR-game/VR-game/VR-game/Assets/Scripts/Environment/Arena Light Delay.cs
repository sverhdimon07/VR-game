using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class ArenaLightDelay : MonoBehaviour
{
    private float intensityDelay = 0.04f;
    private void IncreasingIntensity()
    {
        gameObject.GetComponent<Light>().intensity += 1;
    }
    public void IncreasingDelay()
    {
        for (int i = 0; i < 15; i++)
        {
            Invoke("IncreasingIntensity", intensityDelay * i);
        }
    }
}