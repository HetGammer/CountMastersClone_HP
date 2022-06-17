using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public static class ColliderExtension
    {
        public static bool HasInterface<T>(this Collider col)
        {
            return InterfaceUtility.HasInterface<T>(col.gameObject);
        }
    }
}