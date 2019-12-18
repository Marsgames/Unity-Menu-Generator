using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
<<<<<<< Updated upstream
    public enum ACTIONS { LoadScene, Quit};

    [HideInInspector]
    public int buttonIndex;
    public string sceneName;
    private ACTIONS action;
    [HideInInspector]
    public int actionIndex;
=======
    #region Variables
    private string sceneName;
    private GameObject m_objectToToggle;

    private int m_buttonIndex;
    private EAction m_action = (EAction)2;
    private int m_actionIndex;
    #endregion Variables
>>>>>>> Stashed changes

    private void Start()
    {
        action = ACTIONS.Quit;
    }

    public int GetButtonIndex()
    {
        return buttonIndex;
    }

    public void SetButtonIndex(int index)
    {
        buttonIndex = index;
    }

    public string GetSceneName()
    {
        return sceneName;
    }

    public void SetSceneName(string str)
    {
        sceneName = str;
    }

<<<<<<< Updated upstream
    public ACTIONS GetAction()
=======
        m_objectToToggle = objectToToggle;
    }

    public EAction GetAction()
>>>>>>> Stashed changes
    {
        return action;
    }

    public void SetAction(ACTIONS act)
    {
        action = act;
    }

    public int GetActionIndex()
    {
        return actionIndex;
    }

    public void SetActionIndex(int i)
    {
<<<<<<< Updated upstream
        actionIndex = i;
=======
        m_actionIndex = i;
>>>>>>> Stashed changes
    }
}
