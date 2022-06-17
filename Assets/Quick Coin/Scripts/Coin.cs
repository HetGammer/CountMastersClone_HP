using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header(" Elements ")]
    public new MeshRenderer renderer;
    public new Collider collider;
    public ParticleSystem explodeParticles;

    public void Collect()
    {
        renderer.enabled = false;
        collider.enabled = false;
        explodeParticles.Play();

        Destroy(gameObject, 3);
    }
}
