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

public class MenuManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioClip selectSound = null;

    private List<GameObject> buttons;
    private int currentIndex;
    private int maxIndex;
    private AudioSource audioSource;
    #endregion Variables

    ///////////////////////////////////////////////////////////

    #region Enums
    #endregion Enums

    ///////////////////////////////////////////////////////////

    #region Unity's functions
    // Start is called before the first frame update
    void Start()
    {
        CheckIfOk();

        audioSource = GetComponent<AudioSource>();


        buttons = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Button"))
            {
                buttons.Add(transform.GetChild(i).gameObject);
            }
        }

        currentIndex = 0;
        maxIndex = buttons.Count - 1;

        buttons[currentIndex].GetComponent<Animator>().SetBool("selected", true);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S)) //BAS
        {
            //incrémentation
            currentIndex++;
            if (currentIndex > maxIndex)
            {
                currentIndex = 0;
            }

            SetSelected(currentIndex);
        }
        if (Input.GetKeyDown(KeyCode.Z))//HAUT
        {
            //décrémentation
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = maxIndex;
            }

            SetSelected(currentIndex);
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            ButtonManager btn = buttons[currentIndex].GetComponent<ButtonManager>();
            if (btn.GetAction() == ButtonManager.EAction.LoadScene)
            {
                Debug.Log("load scene");
                SceneManager.LoadScene(btn.GetSceneName());
            }
            else if (btn.GetAction() == ButtonManager.EAction.Quit)
            {
                Application.Quit();
                Debug.Log("quit");
            }
        }

        if (CheckMouseHoverButtons())
        {
            SetSelected(currentIndex);
        }
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

    //regarde si la souris est sur un bouton. Si oui elle renvoie true et change l'index.
    public bool CheckMouseHoverButtons()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            ButtonManager button = raycastResults[i].gameObject.transform.parent.GetComponent<ButtonManager>();
            if (button != null && button.GetButtonIndex() != currentIndex)
            {
                currentIndex = button.GetButtonIndex();
                return true;
            }
        }
        return false;
    }

    public void SetSelected(int index)
    {
        PlaySound(selectSound);

        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == index)
            {
                buttons[i].GetComponent<Animator>().SetBool("selected", true);
            }
            else
            {
                buttons[i].GetComponent<Animator>().SetBool("selected", false);
            }
        }
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
    #endregion Functions

    ///////////////////////////////////////////////////////////

    #region Accessors
    #endregion Accessors
}