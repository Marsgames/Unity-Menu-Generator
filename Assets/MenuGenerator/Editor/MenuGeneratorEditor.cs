using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;

[CustomEditor(typeof(MenuGenerator))]
public class MenuGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MenuGenerator menuGenerator = (MenuGenerator)target;

        if (GUILayout.Button("Generate Menu"))
        {
            menuGenerator.CreateMenu();

            EditorApplication.isPlaying = false;
        }

        if (GUILayout.Button("Clean Menu"))
        {
            // The IEnumerator of Transform uses childCount and GetChild to iterate through the childs. 
            // So modifying the collection while you iterate through them won't work.
            List<Transform> childs = menuGenerator.transform.Cast<Transform>().ToList();
            foreach (Transform child in childs)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        //HideBGVariables();
    }

    // TODO: faire marcher ça
    private void HideBGVariables()
    {
        MenuGenerator mg = target as MenuGenerator;

        mg.SetUseBgImage(EditorGUILayout.Toggle("Use Bg Image", mg.GetUseBgImage()));
        //Debug.Log("m_useBgImage value : " + mg.GetUseBgImage());

        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(mg.GetUseBgImage())))
        {
            if (group.visible == false)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PrefixLabel("Number");
                mg.SetBackgroundPadding(EditorGUILayout.IntSlider(mg.GetBackgroundPadding(), 0, 500));
                EditorGUI.indentLevel--;
            }
        }
    }
}
