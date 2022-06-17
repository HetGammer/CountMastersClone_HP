using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UI;

namespace JetSystems
{
    [CustomEditor(typeof(PaletteElement))]
    public class PaletteElementEditor : Editor
    {
        PaletteElement paletteElement;

        private void OnEnable()
        {
            if (paletteElement == null)
            {
                paletteElement = (PaletteElement)target;
            }
        }

        public override void OnInspectorGUI()
        {
            // Show the color palette object
            Utils.ShowSerializedField(serializedObject, "colorPalette");

            GUILayout.Space(20);
                
            // Show the colors
            if(paletteElement.colorPalette != null)
                ShowPaletteColors();

            if(GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(paletteElement);
            }
        }

        private void ShowPaletteColors()
        {
            ColorPalette colorPalette = paletteElement.colorPalette;

            int colorsPerRow = 5;
            int currentIndex = 0;

            while(currentIndex <= colorPalette.colors.Count)
            {
                int endIndex = Mathf.Min(currentIndex + colorsPerRow, colorPalette.colors.Count);
                ShowColorRow(colorPalette, currentIndex, endIndex);

                currentIndex += colorsPerRow;
            }

        }

        private void ShowColorRow(ColorPalette colorPalette, int startIndex, int endIndex)
        {
            EditorGUILayout.BeginHorizontal();

            for (int i = startIndex; i < endIndex; i++)
            {
                ShowColorButton(colorPalette.colors[i]);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void ShowColorButton(Color color)
        {
            //GUIStyle buttonStyle = EditorStyles.miniButton;
            //buttonStyle.hover.background = Utils.GetColoredTexture(color);

            GUI.backgroundColor = color;

            if(GUILayout.Button(""))
            {
                SetImageColor(color);
            }

            GUI.backgroundColor = Color.white;
        }

        private void SetImageColor(Color color)
        {
            Image image = paletteElement.GetComponent<Image>();

            if (image == null)
            {
                Debug.LogError("No Image component found on this object.");
                return;
            }

            image.color = color;
        }

        
    }
}

#endif