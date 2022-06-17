using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public class BasicCollectable : MonoBehaviour, ICollectable
    {
        [Header(" Elements ")]
        [SerializeField] private new MeshRenderer renderer;
        [SerializeField] private new Collider collider;
        [SerializeField] private ParticleSystem explodeParticles;

        [Header(" Effect ")]
        [SerializeField] private CollectableEffect effect;
        [SerializeField] private float effectValue;

        [Header(" Settings ")]
        private JetCharacter characterWhoCollected;

        public void Collect(JetCharacter characterWhoCollected)
        {
            this.characterWhoCollected = characterWhoCollected;

            Hide();
            PlayExplodeParticles();
            ExecuteCollectableEffect();

            Destroy(gameObject, 3);
        }

        private void Hide()
        {
            renderer.enabled = false;
            collider.enabled = false;
        }

        private void PlayExplodeParticles()
        {
            if (explodeParticles != null)
                explodeParticles.Play();
        }

        private void ExecuteCollectableEffect()
        {
            CollectableManager.onCollectableCollected?.Invoke(characterWhoCollected, effect, effectValue);
        }
    }
}