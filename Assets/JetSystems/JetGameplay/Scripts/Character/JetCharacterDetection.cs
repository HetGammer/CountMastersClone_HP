using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    [RequireComponent(typeof(JetCharacter))]
    public class JetCharacterDetection : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (InterfaceUtility.HasInterface<ICollectable>(other.gameObject))
                other.GetComponent<ICollectable>().Collect(GetComponent<JetCharacter>());
        }
    }
}