using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public static class VectorExtension
    {
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }
    }
}