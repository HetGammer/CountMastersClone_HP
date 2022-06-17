using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public static class TransformExtension
    {
        public static void Clear(this Transform parent)
        {
            while (parent.childCount > 0)
            {
                Transform t = parent.GetChild(0);
                t.SetParent(null);

#if UNITY_EDITOR
                Object.DestroyImmediate(t.gameObject);
#else

                Object.Destroy(t.gameObject);
#endif
            }
        }
    }
}