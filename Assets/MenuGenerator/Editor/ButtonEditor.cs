#pragma warning disable CS0618
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonManager))]
public class ButtonEditor : Editor
{
    [SerializeField] private string m_sceneName;
    [SerializeField] private GameObject m_objectToToggle;
    [SerializeField] private string m_index;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        ButtonManager buttonManager = (ButtonManager)target;
        buttonManager.SetActionIndex(GUILayout.Toolbar(buttonManager.GetActionIndex(), new string[] { "Load Scene", "ToggleObject", "Quit" }));

        buttonManager.SetButtonIndex(buttonManager.GetButtonIndex());

        switch (buttonManager.GetActionIndex())
        {
            case 0:
                buttonManager.SetAction(ButtonManager.EAction.LoadScene);
                m_sceneName = buttonManager.GetSceneName();

                GUILayout.BeginHorizontal();
                if (!Application.isPlaying)
                {
                    GUILayout.Label("Scene Name : ", GUILayout.Width(80));
                    m_sceneName = GUILayout.TextField(m_sceneName);
                    buttonManager.SetSceneName(m_sceneName);
                }

                GUILayout.EndHorizontal();
                break;

            case 1:
                buttonManager.SetAction(ButtonManager.EAction.ToggleObject);
                m_objectToToggle = buttonManager.GetObjectToToggle();

                GUILayout.BeginHorizontal();
                if (!Application.isPlaying)
                {
                    GUILayout.Label("Object to toggle", GUILayout.Width(100));
                    m_objectToToggle = (GameObject)EditorGUILayout.ObjectField(m_objectToToggle, typeof(GameObject));
                    buttonManager.SetObjectToToggle(m_objectToToggle);
                }

                GUILayout.EndHorizontal();
                break;

            case 2:
                buttonManager.SetAction(ButtonManager.EAction.Quit);
                break;
        }
    }
}
