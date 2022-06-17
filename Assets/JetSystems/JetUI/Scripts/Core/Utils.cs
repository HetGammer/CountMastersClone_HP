using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UI;
#endif

namespace JetSystems
{


    public static class Utils
    {

        public static Vector2 GetScreenCenter()
        {
            return new Vector2((float)Screen.width / 2, (float)Screen.height / 2);
        }

        public static Transform GetClosestTransformInArray(Transform source, Transform[] objectsArray, float limitDistance = 50000)
        {
            int closestIndex = -1;
            float closestDistance = limitDistance;

            for (int i = 0; i < objectsArray.Length; i++)
            {
                float distance = Vector3.Distance(source.position, objectsArray[i].position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            if (closestIndex == -1) return null;

            return objectsArray[closestIndex];
        }

        public static int GetClosestVectorIndexInArray(Vector3 source, Vector3[] objectsArray, float limitDistance = 50000)
        {
            int closestIndex = -1;
            float closestDistance = limitDistance;

            for (int i = 0; i < objectsArray.Length; i++)
            {
                float distance = Vector3.Distance(source, objectsArray[i]);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            if (closestIndex == -1) return -1;

            return closestIndex;
        }

        public static Transform[] ColliderToTransformArray(Collider[] colliders)
        {
            Transform[] transforms = new Transform[colliders.Length];
            for (int i = 0; i < colliders.Length; i++)
            {
                transforms[i] = colliders[i].transform;
            }
            return transforms;
        }

        public static Texture2D LoadTexture(string path)
        {
            if (!File.Exists(path)) { Debug.Log("File does not exist "); return null; }
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D result = new Texture2D(1, 1);
            FileInfo finfo = new FileInfo(path);
            result.name = finfo.Name;
            result.LoadImage(bytes);
            return result;
        }

        // Returns a random gameobject from an array
        public static GameObject GetRandomGameobjectFromArray(GameObject[] objectsArray)
        {
            // Initialize the random gameobject
            GameObject randomGameobject;

            // Get a random index
            int randomIndex = Random.Range(0, objectsArray.Length);

            // Store the random gameobject
            randomGameobject = objectsArray[randomIndex];

            // Return it
            return randomGameobject;
        }

        // Returns a random gameobject from a list
        public static GameObject GetRandomGameobjectFromList(List<GameObject> objectsArray)
        {
            // Initialize the random gameobject
            GameObject randomGameobject;

            // Get a random index
            int randomIndex = Random.Range(0, objectsArray.Count);

            // Store the random gameobject
            randomGameobject = objectsArray[randomIndex];

            // Return it
            return randomGameobject;
        }

        // Format an int to string with a space each thousand
        public static string FormatAmountString(int amount)
        {
            string formattedAmount = "";

            if (amount < 1000)
            {
                formattedAmount = amount.ToString();
            }
            else if (amount >= 1000 && amount < 1000000)
            {
                formattedAmount = (amount / 1000).ToString() + " " + amount.ToString().Substring(amount.ToString().Length - 3, 3);
            }

            return formattedAmount;
        }

        // Returns the screen ratio
        public static float GetScreenRatio()
        {
            float w = Screen.currentResolution.width;
            float h = Screen.currentResolution.height;

            float ratio = w / h;

            return ratio;
        }

        public static void EnableCG(CanvasGroup cg)
        {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        public static void DisableCG(CanvasGroup cg)
        {
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }

        public static void HideAllCGs(CanvasGroup[] cgs, CanvasGroup toShow = null)
        {
            for (int i = 0; i < cgs.Length; i++)
            {
                if (cgs[i] == toShow)
                    EnableCG(toShow);
                else
                    DisableCG(cgs[i]);
            }
        }

#if UNITY_EDITOR
        public static void CategoryHeader(string categoryTitle, int spacing = 20, GUIStyle style = null)
        {
            // Configure the styles
            if (style == null)
            {
                style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.fontSize = 14;
                style.fontStyle = FontStyle.BoldAndItalic;
            }

            GUILayout.Space(spacing);

            EditorGUILayout.LabelField(categoryTitle, style);

            GUILayout.Space(spacing);
        }

        public static void ShowSerializedField(SerializedObject serializedObject, string fieldName, string label = "")
        {
            if (label == "")
                label = serializedObject.FindProperty(fieldName).displayName;

            EditorGUILayout.PropertyField(serializedObject.FindProperty(fieldName), new GUIContent(label));
        }
#endif

        public static Texture2D GetColoredTexture(Color color)
        {
            int width = 100;
            int height = 100;

            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = color;
           

            Texture2D tex = new Texture2D(width, height);
            tex.SetPixels(colors);

            return tex;
        }

        public static void Rate()
        {
#if UNITY_IOS
        UnityEngine.iOS.Device.RequestStoreReview();
#elif UNITY_ANDROID
            string packageName = Application.identifier;
            Application.OpenURL("https://play.google.com/store/apps/details?id=" + packageName);
#endif
        }
    }


}