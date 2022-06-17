using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public static class InterfaceUtility
    {
        public static bool HasInterface<T>(GameObject objectToInspect)
        {
            MonoBehaviour[] monos = objectToInspect.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour mono in monos)
                if (mono is T)
                    return true;

            return false;
        }

    }
}
