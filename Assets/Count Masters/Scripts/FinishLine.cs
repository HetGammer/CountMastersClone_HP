using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [Header(" Particles ")]
    [SerializeField] private ParticleSystem[] confettis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayConfettiParticles()
    {
        foreach (ParticleSystem ps in confettis)
            ps.Play();

        Audio_Manager.instance.Play("Level_Complete");
    }
}
