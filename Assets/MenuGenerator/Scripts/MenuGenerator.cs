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
using System.Linq;
using UnityEditor;

public class MenuGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool m_useBackgroundImage = false;

    [SerializeField, Tooltip("This one can be empty")] private ButtonGenerator m_buttonAspect = null;
    [SerializeField, Tooltip("This one can be empty")] public Object m_backgroundAspect = null;

    private GameObject m_buttonPrefab;
    private GameObject m_backgroundPrefab;

    private VerticalLayoutGroup m_vLayout;
    private CanvasScaler m_cScaler;
    private GameObject m_background;

    [Header("Réglages :")]
    [SerializeField] private string[] m_buttonNames = null;

    [Space]
    [SerializeField, Range(-500, 500), Tooltip("Change this value after creating buttons menu")] private int m_spaceBewteenButtons = 0;
    [SerializeField, Range(0, 500), Tooltip("Change this value after generating menu")] private int m_backgroundPadding = 0;
    [SerializeField] private float m_buttonFontSize = 36;

    private List<GameObject> m_buttons;
    #endregion Variables

    ///////////////////////////////////////////////////////////

    #region Unity's functions
    private void OnValidate()
    {

        if (m_vLayout)// && transform.childCount > 0)
        {
            m_vLayout.spacing = m_spaceBewteenButtons;
            //m_vLayout.padding = new RectOffset(0, 0, 0, m_spaceBewteenButtons);
        }
        else
        {
            m_vLayout = GetComponent<VerticalLayoutGroup>();

        }

        if (m_background)
        {
            RectTransform rect = m_background.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(m_backgroundPadding, m_backgroundPadding);
            rect.offsetMax = new Vector2(-m_backgroundPadding, -m_backgroundPadding);
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
        //#if UNITY_EDITOR
        //        bool isOk = true;
        //
        //        //if (!m_gameLogoPrefab)
        //        //{
        //        //    Debug.LogError("<b>Game Logo Prefab</b> cannot be null in <color=#0000FF>" + name + "</color>", gameObject);
        //        //    isOk = false;
        //        //}
        //
        //        UnityEditor.EditorApplication.isPlaying = isOk;
        //#endif
    }

    public void CreateMenu()
    {
        m_vLayout = GetComponent<VerticalLayoutGroup>();
        m_vLayout.spacing = m_spaceBewteenButtons;

        m_cScaler = GetComponent<CanvasScaler>();
        m_cScaler.matchWidthOrHeight = 1;
        m_cScaler.referenceResolution = new Vector2(Screen.width, Screen.height);

        // Generate prefabs
        GenerateButtonPrefab();
        GenerateBackgroundPrefab();

        m_buttons = new List<GameObject>();

        //Clear du menu
        List<Transform> childs = transform.Cast<Transform>().ToList();
        foreach (Transform child in childs)
        {
            DestroyImmediate(child.gameObject);
        }

        //Background Image
        m_background = Instantiate(m_backgroundPrefab, transform);

        //Boutons
        int index = 0;
        foreach (string buttonName in m_buttonNames)
        {
            GameObject button = Instantiate(m_buttonPrefab, transform);
            SetButtonText(button, buttonName);
            button.GetComponent<ButtonManager>().SetButtonIndex(index++);
            m_buttons.Add(button);
        }

        NormalizeButtonSize();
    }

    //change le text du bouton
    public void SetButtonText(GameObject button, string buttonText)
    {
        Transform child = button.transform.Find("ButtonBG"); // TODO: améliorer la recherche
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
        }


        rectTransform = button.transform.Find("ButtonBG").GetComponent<RectTransform>(); // TODO: améliorer la recherche
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
            child = button.transform.Find("ButtonBG"); // TODO: améliorer la recherche

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

        foreach (GameObject button in m_buttons)
        {
            child = button.transform.Find("ButtonBG"); // TODO: améliorer la recherche

            if (child)
            {
                rectTransform = child.GetComponent<RectTransform>();

                // ça évite de parcourir 2 fois la totalité des enfants
                if (rectTransform.sizeDelta.x == maxWidth)
                {
                    return;
                }

                rectTransform.sizeDelta = new Vector2(maxWidth, rectTransform.sizeDelta.y);
            }
        }
    }

    private void GenerateButtonPrefab()
    {
        m_buttonPrefab = Resources.Load<GameObject>("Button");

        if (m_buttonAspect)
        {
            Image buttonImage = m_buttonPrefab.GetComponentInChildren<Image>();
            buttonImage.sprite = m_buttonAspect.GetImage();
            buttonImage.color = m_buttonAspect.GetColor();
        }
    }

    private void GenerateBackgroundPrefab()
    {
        m_backgroundPrefab = Resources.Load<GameObject>("Background");
        LayoutElement le = m_backgroundPrefab.AddComponent<LayoutElement>();
        le.ignoreLayout = true;

        if (m_backgroundAspect)
        {
            BackgroundGenerator bgAspect = m_backgroundAspect as BackgroundGenerator;
            Image backgroundImage = m_backgroundPrefab.GetComponentInChildren<Image>();
            backgroundImage.sprite = bgAspect.GetImage();
            backgroundImage.color = bgAspect.GetColor();
        }

        Vector2 screeSize = new Vector2(Screen.width, Screen.height);
        RectTransform rect = m_backgroundPrefab.GetComponent<RectTransform>();
        rect.sizeDelta = screeSize;
        rect.offsetMin = new Vector2(m_backgroundPadding, m_backgroundPadding);
        rect.offsetMax = new Vector2(m_backgroundPadding, m_backgroundPadding);
    }
    #endregion Functions

    ///////////////////////////////////////////////////////////

    #region Accessors
    public bool GetUseBackgroundImage()
    {
        return m_useBackgroundImage;
    }
    public void SetUseBackgroundImage(bool value)
    {
        m_useBackgroundImage = value;
    }
    #endregion Accessors
}

[CustomEditor(typeof(MenuGenerator))]
public class MenuGeneratorEditor : Editor
{
    override public void OnInspectorGUI()
    {
        MenuGenerator mg = target as MenuGenerator;

        mg.SetUseBackgroundImage(GUILayout.Toggle(mg.GetUseBackgroundImage(), "UseBackgroundImage"));
        //myScript.flag = GUILayout.Toggle(myScript.flag, "Flag");

        if (mg.GetUseBackgroundImage())
        {
#pragma warning disable CS0618
            mg.m_backgroundAspect = EditorGUILayout.ObjectField("Background Aspect", mg.m_backgroundAspect, null);
        }

        //if (myScript.flag)
        //myScript.i = EditorGUILayout.IntSlider("I field:", myScript.i, 1, 100);

    }
}