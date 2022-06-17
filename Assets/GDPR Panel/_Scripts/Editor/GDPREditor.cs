using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GDPR))]
public class GDPREditor : Editor
{
    GDPR gdpr;
    bool advanced;

    private void OnEnable()
    {
        if (gdpr == null)
            gdpr = (GDPR)target;
    }

    public override void OnInspectorGUI()
    {
        //Undo.RecordObject(gdpr, "Name Changed");
        //gdpr.gameName = EditorGUILayout.TextField("Game Name", gdpr.gameName);

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameName"));
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(gdpr, "Name Changed");
            serializedObject.ApplyModifiedProperties();
            gdpr.UpdateTexts();
            EditorUtility.SetDirty(gdpr);
        }

        if (advanced)
        {
            gdpr.titleText = EditorGUILayout.ObjectField("Title", gdpr.titleText, typeof(Text), true) as Text;
            gdpr.consentText = EditorGUILayout.ObjectField("Consent Text", gdpr.consentText, typeof(Text), true) as Text;
        }

        advanced = EditorGUILayout.Toggle("Advanced", advanced);

        if (GUI.changed)
        {
            gdpr.UpdateTexts();
            EditorUtility.SetDirty(gdpr);            
        }
    }
}
#endif