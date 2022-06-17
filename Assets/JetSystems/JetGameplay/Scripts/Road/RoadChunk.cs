using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public class RoadChunk : MonoBehaviour
    {
        [Header(" Chunks Settings ")]
        public float length;

        [Header(" Gizmos ")]
        public Color gizmosColor;

        private void OnDrawGizmos()
        {

            Vector3 center = transform.position + (length / 2 * Vector3.forward);
            Vector3 size = new Vector3(20, 20, length);

            Gizmos.color = gizmosColor;
            Gizmos.DrawWireCube(center, size);

        }
    }
}