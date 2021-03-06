﻿#region Author
/////////////////////////////////////////
//   Author : leomani3
//   Source : https://github.com/leomani3/Unity-Menu-Generator
/////////////////////////////////////////
#endregion
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup), typeof(CanvasScaler))]
public class MenuGenerator : MonoBehaviour
{
    #region Variables
    [Header("Background Image")]
    [SerializeField] private bool m_useBgImage = false;
    [SerializeField, Tooltip("This one can be empty") /*, ConditionalField(nameof(m_useBgImage))*/ ] private BackgroundGenerator m_backgroundAspect = null;
    [SerializeField, Range(0, 500), Tooltip("Change this value after generating menu")] private int m_backgroundPadding = 0;

    [Header("Buttons")]
    [SerializeField, Tooltip("This one can be empty")] private ButtonGenerator m_buttonAspect = null;
    [SerializeField] private string[] m_buttonNames = null;
    [SerializeField] private float m_buttonFontSize = 36;
    [SerializeField, Range(-15, 400), Tooltip("Change this value after creating buttons menu")] private int m_spaceBewteenButtons = 0;

    private GameObject m_buttonPrefab;
    private GameObject m_backgroundPrefab;

    private VerticalLayoutGroup m_vLayout;
    private CanvasScaler m_cScaler;
    private GameObject m_background;

    private List<GameObject> m_buttons;
    #endregion Variables

    ///////////////////////////////////////////////////////////

    #region Unity's functions
    private void OnValidate()
    {

        if (!m_vLayout)
        {
            m_vLayout = GetComponent<VerticalLayoutGroup>();
        }
        m_vLayout.spacing = m_spaceBewteenButtons;

        if (m_background)
        {
            RectTransform rect = m_background.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(m_backgroundPadding, m_backgroundPadding);
            rect.offsetMax = new Vector2(-m_backgroundPadding, -m_backgroundPadding);
        }

        if (!m_cScaler)
        {
            m_cScaler = GetComponent<CanvasScaler>();
        }
        m_cScaler.matchWidthOrHeight = 1;
        m_cScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
    }

    private void Start()
    {
        if (!m_cScaler)
        {
            m_cScaler = GetComponent<CanvasScaler>();
        }
        m_cScaler.matchWidthOrHeight = 1;
        m_cScaler.referenceResolution = new Vector2(Screen.width, Screen.height);


        if (!m_vLayout)
        {
            m_vLayout = GetComponent<VerticalLayoutGroup>();
        }
        m_vLayout.spacing = m_spaceBewteenButtons;
    }
    #endregion Unity 's functions

    ///////////////////////////////////////////////////////////

    #region Functions
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
        CheckBackground();

        //Boutons
        int index = 0;
        foreach (string buttonName in m_buttonNames)
        {
            GameObject button = Instantiate(m_buttonPrefab, transform);
            button.name = "Button-" + buttonName;
            SetButtonText(button, buttonName);
            button.GetComponent<ButtonManager>().SetButtonIndex(index++);
            m_buttons.Add(button);
        }

        NormalizeButtonSize();
    }

    //change le text du bouton
    public void SetButtonText(GameObject button, string buttonText)
    {
        Transform child = button.transform.Find("ButtonBG");
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

        rectTransform = button.transform.Find("ButtonBG").GetComponent<RectTransform>();
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
            child = button.transform.Find("ButtonBG");

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
            child = button.transform.Find("ButtonBG");

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
        LayoutElement le = m_backgroundPrefab.GetComponent<LayoutElement>();
        if (!le)
        {
            le = m_backgroundPrefab.AddComponent<LayoutElement>();
        }
        le.ignoreLayout = true;

        if (m_backgroundAspect)
        {
            Image backgroundImage = m_backgroundPrefab.GetComponentInChildren<Image>();
            backgroundImage.sprite = m_backgroundAspect.GetImage();
            backgroundImage.color = m_backgroundAspect.GetColor();
        }

        Vector2 screeSize = new Vector2(Screen.width, Screen.height);
        RectTransform rect = m_backgroundPrefab.GetComponent<RectTransform>();
        rect.sizeDelta = screeSize;
        rect.offsetMin = new Vector2(m_backgroundPadding, m_backgroundPadding);
        rect.offsetMax = new Vector2(-m_backgroundPadding, -m_backgroundPadding);
    }

    private void CheckBackground(bool fromOnValidate = false)
    {
        if (m_useBgImage)
        {
            if (m_background)
            {
                return;
            }

            GenerateBackgroundPrefab();
            m_background = Instantiate(m_backgroundPrefab, transform);
        }
        else
        {
            if (!m_background)
            {
                return;
            }

            DestroyImmediate(m_background);
        }
    }
    #endregion Functions

    ///////////////////////////////////////////////////////////

    #region Accessors
    public bool GetUseBgImage()
    {
        return m_useBgImage;
    }
    public void SetUseBgImage(bool value)
    {
        m_useBgImage = value;
    }

    public int GetBackgroundPadding()
    {
        return m_backgroundPadding;
    }
    public void SetBackgroundPadding(int padding)
    {
        m_backgroundPadding = padding;
    }
    #endregion Accessors
}