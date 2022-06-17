using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public class CollectableManager : MonoBehaviour
    {
        public delegate void OnCollectableCollected(JetCharacter characterWhoCollected, CollectableEffect collectableEffect, float collectableEffectValue);
        public static OnCollectableCollected onCollectableCollected;

        private void Awake()
        {
            onCollectableCollected += OnCollectableCollectedCallback;
        }

        private void OnCollectableCollectedCallback(JetCharacter characterWhoCollected, CollectableEffect collectableEffect, float collectableEffectValue)
        {

            switch(characterWhoCollected.GetCharacterType())
            {
                case JetCharacter.CharacterType.Player:
                    OnPlayerCollected(characterWhoCollected, collectableEffect, collectableEffectValue);
                    break;

                case JetCharacter.CharacterType.Bot:
                    OnBotCollected(characterWhoCollected, collectableEffect, collectableEffectValue);
                    break;

                default:
                    break;
            }

            
        }

        private void OnPlayerCollected(JetCharacter characterWhoCollected, CollectableEffect collectableEffect, float collectableEffectValue)
        {
            switch (collectableEffect)
            {
                case CollectableEffect.AddCoins:
                    CollectCoins((int)collectableEffectValue);
                    break;

                case CollectableEffect.IncreaseSize:
                    IncreaseSize(characterWhoCollected, collectableEffectValue);
                    break;

                case CollectableEffect.DecreaseSize:
                    DecreaseSize(characterWhoCollected, collectableEffectValue);
                    break;
            }
        }

        private void OnBotCollected(JetCharacter characterWhoCollected, CollectableEffect collectableEffect, float collectableEffectValue)
        {
            switch (collectableEffect)
            {
                case CollectableEffect.IncreaseSize:
                    IncreaseSize(characterWhoCollected, collectableEffectValue);
                    break;

                case CollectableEffect.DecreaseSize:
                    DecreaseSize(characterWhoCollected, collectableEffectValue);
                    break;
            }
        }

        private void CollectCoins(int coinsAmount)
        {
            UIManager.AddCoins(coinsAmount);
        }

        private void IncreaseSize(JetCharacter characterWhoCollected, float sizeIncreaseValue)
        {
            characterWhoCollected.IncreaseSize(sizeIncreaseValue);
        }

        private void DecreaseSize(JetCharacter characterWhoCollected, float sizeDecreaseValue)
        {
            characterWhoCollected.DecreaseSize(sizeDecreaseValue);
        }
    }
}