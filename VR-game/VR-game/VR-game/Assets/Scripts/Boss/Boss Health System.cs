using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{
    public static float health = 100f;
    void Update()
    {

    }
    public static void HealthRegeneration()
    {
        health = 0;
        health += 100f;
    }
}