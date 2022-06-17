using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOneParticleSystem : MonoBehaviour
{
    static PlusOneParticleSystem instance;

    [Header(" Particle Systems ")]
    public ParticleSystem plusOneParticleSystem;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static void PlayPlusOneParticles(Vector3 position)
    {
        instance.plusOneParticleSystem.transform.position = position;
        instance.plusOneParticleSystem.Play();
    }
}
