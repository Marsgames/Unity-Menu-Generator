﻿#region Author
/////////////////////////////////////////
//   Author : leomani3
//   Source : https://github.com/leomani3/Unity-Menu-Generator
/////////////////////////////////////////
#endregion
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    #region Variables
    [SerializeField, HideInInspector] private string sceneName;
    [SerializeField, HideInInspector] private GameObject m_objectToToggle;

    [SerializeField, HideInInspector] private int m_buttonIndex;
    [SerializeField, HideInInspector] private EAction m_action;
    [SerializeField, HideInInspector] private int m_actionIndex;
    #endregion Variables

    ///////////////////////////////////////////////////////////

    #region Enums
    public enum EAction { LoadScene, ToggleObject, Quit }
    #endregion Enums

    ///////////////////////////////////////////////////////////

    #region Unity's functions
    #endregion Unity 's functions

    ///////////////////////////////////////////////////////////

    #region Functions
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

    public void SetObjectToToggle(GameObject objectToToggle)
    {
        if (!objectToToggle)
        {
            return;
        }

        m_objectToToggle = objectToToggle;
    }
    public GameObject GetObjectToToggle()
    {
        return m_objectToToggle;
    }

    public EAction GetAction()
    {
        return m_action;
    }
    public void SetAction(EAction action)
    {
        m_action = action;
    }

    //public int GetActionIndex()
    //{
    //    return m_actionIndex;
    //}
    //public void SetActionIndex(int i)
    //{
    //    m_actionIndex = i;
    //}
    #endregion Accessors
}