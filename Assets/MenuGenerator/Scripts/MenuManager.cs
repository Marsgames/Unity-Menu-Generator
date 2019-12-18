#region Author
/////////////////////////////////////////
//   Author : leomani3
//   Source : https://github.com/leomani3/Unity-Menu-Generator
/////////////////////////////////////////
#endregion
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioClip m_selectSound = null;

    private List<GameObject> m_buttons = new List<GameObject>();
    private int m_currentIndex;
    private int m_maxIndex;
    private AudioSource m_audioSource;
    private Camera m_camera;

    private int m_lastAction = 0; // Use to allow selection by keyboard AND unselect when mouse is out of button
    #endregion Variables

    ///////////////////////////////////////////////////////////

    #region Unity's functions
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_camera = Camera.main;


        foreach (Transform child in transform)
        {
            if (child.name.Contains("Button"))
            {
                m_buttons.Add(child.gameObject);
            }
        }

        m_currentIndex = -1;
        m_maxIndex = m_buttons.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        MoveInMenu();

        ButtonAction();

        if (CheckMouseHoverButtons())
        {
            SetSelected(m_currentIndex);
        }
    }
    #endregion Unity's functions

    ///////////////////////////////////////////////////////////

    #region Functions
    private void MoveInMenu()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_currentIndex--;
            if (m_currentIndex < 0)
            {
                m_currentIndex = m_maxIndex;
            }

            SetSelected(m_currentIndex);
            m_lastAction = 0;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_currentIndex++;
            if (m_currentIndex > m_maxIndex)
            {
                m_currentIndex = 0;
            }

            SetSelected(m_currentIndex);
            m_lastAction = 0;
        }
    }

    private void ButtonAction()
    {
        if (Input.GetKeyDown(KeyCode.Return) || (Input.GetMouseButtonDown(0) && 1 == m_lastAction))
        {
            ButtonManager btn = m_buttons[m_currentIndex].GetComponent<ButtonManager>();

            if (!btn)
            {
                return;
            }

            switch (btn.GetAction())
            {
                case ButtonManager.EAction.LoadScene:
                    SceneManager.LoadScene(btn.GetSceneName());
                    break;

                case ButtonManager.EAction.ToggleObject:
                    GameObject go = btn.GetObjectToToggle();
                    if (go)
                    {
                        go.SetActive(!go.activeSelf);
                    }
                    break;

                case ButtonManager.EAction.Quit:
                    Application.Quit();

#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    break;
            }
        }
    }

    //regarde si la souris est sur un bouton. Si oui elle renvoie true et change l'index.
    private bool CheckMouseHoverButtons()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        ButtonManager button;
        foreach (RaycastResult raycast in raycastResults)
        {
            if (!raycast.gameObject.name.Contains("Button"))
            {
                continue;
            }

            button = raycast.gameObject.transform.parent.GetComponent<ButtonManager>();

            if (button && button.GetButtonIndex() != m_currentIndex)
            {
                m_lastAction = 1;

                m_currentIndex = button.GetButtonIndex();
                return true;
            }
            else if (button)
            {
                return false;
            }
        }

        if (1 == m_lastAction)
        {
            m_currentIndex = -1;
            UnselectAll();
        }
        return false;
    }


    private void SetSelected(int index)
    {
        PlaySound(m_selectSound);

        Animator animator;
        foreach (GameObject button in m_buttons)
        {
            animator = button.GetComponent<Animator>();

            if (animator)
            {
                animator.SetBool("selected", false);
            }
        }

        if (index > m_buttons.Count - 1 || index < 0)
        {
            return;
        }

        animator = m_buttons[index].GetComponent<Animator>();

        if (animator)
        {
            animator.SetBool("selected", true);
        }
    }

    private void UnselectAll()
    {
        Animator animator;
        foreach (GameObject button in m_buttons)
        {
            animator = button.GetComponent<Animator>();

            if (animator)
            {
                animator.SetBool("selected", false);
            }
        }
    }

    private void PlaySound(AudioClip sound)
    {
        m_audioSource.clip = sound;
        m_audioSource.Play();
    }
    #endregion Functions

    ///////////////////////////////////////////////////////////

    #region Accessors
    #endregion Accessors
}