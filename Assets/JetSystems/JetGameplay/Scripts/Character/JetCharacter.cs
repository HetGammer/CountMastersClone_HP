using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public class JetCharacter : MonoBehaviour
    {
        public enum CharacterType { Player, Bot }
        [SerializeField] private CharacterType characterType;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void IncreaseSize(float sizeIncreaseValue)
        {
            transform.localScale += sizeIncreaseValue * Vector3.one;
        }

        public void DecreaseSize(float sizeIncreaseValue)
        {
            transform.localScale -= sizeIncreaseValue * Vector3.one;
        }

        public CharacterType GetCharacterType()
        {
            return characterType;
        }
    }
}