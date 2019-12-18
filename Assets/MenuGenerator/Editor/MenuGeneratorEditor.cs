using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

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
    }
}
