#region Author
/////////////////////////////////////////
//   Author : leomani3
//   Source : https://github.com/leomani3/Unity-Menu-Generator
/////////////////////////////////////////
#endregion
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private string sceneName;

    private int m_buttonIndex;
    private EAction m_action;
    private int m_actionIndex;
    #endregion Variables

    ///////////////////////////////////////////////////////////

    #region Enums
    public enum EAction { LoadScene, Quit };
    #endregion Enums

    ///////////////////////////////////////////////////////////

    #region Unity's functions
    // Start is called before the first frame update
    private void Start()
    {
        CheckIfOk();

        m_action = EAction.Quit;
    }
    #endregion Unity's functions

    ///////////////////////////////////////////////////////////

    #region Functions
    /// <summary>
    /// Checks if all variables are set correctly, otherwise close Editor
    /// </summary>
    private void CheckIfOk()
    {
#if UNITY_EDITOR
        bool isOk = true;

        //if (!m_randomVariable)
        //{
        //    Debug.LogError("<b>Random Variable</b> cannot be null in <color=#0000FF>" + name + "</color>", gameObject);
        //    isOk = false;
        //}

        UnityEditor.EditorApplication.isPlaying = isOk;
#endif
    }
    #endregion Functions

    ///////////////////////////////////////////////////////////

    #region Accessors
    public int GetButtonIndex()
    {
        return m_buttonIndex;
    }
    public void SetButtonIndex(int index)
    {
        m_buttonIndex = index;
    }

    public string GetSceneName()
    {
        return sceneName;
    }
    public void SetSceneName(string name)
    {
        sceneName = name;
    }

    public EAction GetAction()
    {
        return m_action;
    }
    public void SetAction(EAction action)
    {
        m_action = action;
    }

    public int GetActionIndex()
    {
        return m_actionIndex;
    }
    public void SetActionIndex(int i)
    {
        m_actionIndex = i;
    }
    #endregion Accessors
}