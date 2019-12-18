#region Author
/////////////////////////////////////////
//   Author : leomani3
//   Source : https://github.com/leomani3/Unity-Menu-Generator
/////////////////////////////////////////
#endregion
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuGenerator : MonoBehaviour
{
    #region Variables
    //[Header("GameArts")]
    //[SerializeField] private Image m_gameLogo = null;


    [Header("Prefabs :")]
    [SerializeField] private GameObject m_gameLogoPrefab = null;
    [SerializeField] private GameObject m_backgroundPrefab = null;
    [SerializeField] private GameObject m_buttonPrefab = null;

    [Header("Réglages :")]
    [SerializeField] private string[] m_buttonNames = null;

    [Header("")]
    [SerializeField] private float m_gameLogoMarginTop = 0;
    [SerializeField] private float m_gameLogoMarginRight = 0;
    [SerializeField] private float m_gameLogoSize = 0;

    [Header("")]
    [SerializeField] private float m_buttonMarginBottom = 0;
    [SerializeField] private float m_buttonMarginRight = 0;
    [SerializeField] private float m_spaceBewteenButtons = 0;
    [SerializeField] private float m_buttonFontSize = 0;

    private List<GameObject> m_buttons;
    #endregion Variables

    ///////////////////////////////////////////////////////////

    #region Unity's functions
    // Start is called before the first frame update
    private void Start()
    {
        CheckIfOk();

    }
    #endregion Unity's functions

    ///////////////////////////////////////////////////////////

    #region Functions
    /// <summary>
    /// Checks if all variables are set correctly, otherwise close Editor
    /// </summary>
    private bool CheckIfOk()
    {
        bool isOk = true;

#if UNITY_EDITOR
        if (!m_gameLogoPrefab)
        {
            Debug.LogError("<b>Game Logo Prefab</b> cannot be null in <color=#0000FF>" + name + "</color>", gameObject);
            isOk = false;
        }
        if (!m_backgroundPrefab)
        {
            Debug.LogError("<b>Background Prefab</b> cannot be null in <color=#0000FF>" + name + "</color>", gameObject);
            isOk = false;
        }
        if (!m_buttonPrefab)
        {
            Debug.LogError("<b>Button Prefab</b> cannot be null in <color=#0000FF>" + name + "</color>", gameObject);
            isOk = false;
        }


        UnityEditor.EditorApplication.isPlaying = isOk;
#endif

        return isOk;
    }

    public void CreateMenu()
    {
        if (!CheckIfOk())
        {
            return;
        }

        m_buttons = new List<GameObject>();
        //Clear du menu
        int nbChildren = transform.childCount;
        for (int i = 0; i < nbChildren; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        //Background Image
        Instantiate(m_backgroundPrefab, new Vector3(GetComponent<RectTransform>().rect.width / 2, GetComponent<RectTransform>().rect.height / 2, 0), Quaternion.identity, transform);

        //Game Logo
        GameObject gameLogo = Instantiate(m_gameLogoPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
        gameLogo.transform.position = new Vector3(GetComponent<RectTransform>().rect.width / 2 - m_gameLogoMarginRight, GetComponent<RectTransform>().rect.height - gameLogo.GetComponent<RectTransform>().rect.height / 2 - m_gameLogoMarginTop, 0);
        gameLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(m_gameLogoSize, m_gameLogoSize);

        //Boutons
        for (int i = 0; i < m_buttonNames.Length; i++)
        {
            GameObject button = Instantiate(m_buttonPrefab, new Vector3(GetComponent<RectTransform>().rect.width / 2 - m_buttonMarginRight, GetComponent<RectTransform>().rect.height / 2, 0), Quaternion.identity, transform);
            SetButtonText(button, m_buttonNames[i]);
            button.transform.position += new Vector3(0, m_buttonMarginBottom - (i * m_spaceBewteenButtons), 0);
            button.GetComponent<ButtonManager>().SetButtonIndex(i);
            m_buttons.Add(button);
        }

        NormalizeButtonSize();
    }

    //change le text du bouton
    public void SetButtonText(GameObject button, string buttonText)
    {
        Transform child = button.transform.Find("BG");
        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        if (text)
        {
            text.fontSize = m_buttonFontSize;
            text.SetText(buttonText);

            if (rectTransform)
            {
                rectTransform.sizeDelta = new Vector2(m_buttonFontSize * buttonText.Length, m_buttonFontSize);
            }
            else
            {
                Debug.LogWarning("Je me suis chié dans le getcomponent");
            }
        }


        rectTransform = button.transform.Find("BG").GetComponent<RectTransform>();
        if (rectTransform)
        {
            rectTransform.sizeDelta = new Vector2(m_buttonFontSize * buttonText.Length, 2 * m_buttonFontSize);
        }
    }

    //Change la size de tous les boutons pour être de la même largeur que le plus grand bouton
    public void NormalizeButtonSize()
    {
        float maxWidth = 0;
        Transform child;
        RectTransform rectTransform;

        foreach (GameObject button in m_buttons)
        {
            child = button.transform.Find("BG");

            if (child)
            {
                rectTransform = child.GetComponent<RectTransform>();

                if (rectTransform.sizeDelta.x > maxWidth)
                {
                    maxWidth = rectTransform.sizeDelta.x;
                }

                rectTransform.sizeDelta = new Vector2(maxWidth, rectTransform.sizeDelta.y);
            }
        }
    }
    #endregion Functions

    ///////////////////////////////////////////////////////////

    #region Accessors
    #endregion Accessors
}