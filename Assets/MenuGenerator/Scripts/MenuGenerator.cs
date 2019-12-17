#region Author
/////////////////////////////////////////
//   Author : leomani3
//   Source : https://github.com/leomani3/Unity-Menu-Generator
/////////////////////////////////////////
#endregion
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuGenerator : MonoBehaviour
{
    #region Variables
    [Header("Prefabs :")]
    [SerializeField] private GameObject gameLogoPrefab = null;
    [SerializeField] private GameObject backgroundPrefab = null;
    [SerializeField] private GameObject buttonPrefab = null;

    [Header("Réglages :")]
    [SerializeField] private string[] buttonNames = null;

    [Header("")]
    [SerializeField] private float gameLogoMarginTop = 0;
    [SerializeField] private float gameLogoMarginRight = 0;
    [SerializeField] private float gameLogoSize = 0;

    [Header("")]
    [SerializeField] private float buttonMarginBottom = 0;
    [SerializeField] private float buttonMarginRight = 0;
    [SerializeField] private float spaceBewteenButtons = 0;
    [SerializeField] private float buttonFontSize = 0;

    private List<GameObject> buttons;
    #endregion Variables

    ///////////////////////////////////////////////////////////

    #region Enums
    #endregion Enums

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

    public void CreateMenu()
    {
        buttons = new List<GameObject>();
        //Clear du menu
        int nbChildren = transform.childCount;
        for (int i = 0; i < nbChildren; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        //Background Image
        Instantiate(backgroundPrefab, new Vector3(GetComponent<RectTransform>().rect.width / 2, GetComponent<RectTransform>().rect.height / 2, 0), Quaternion.identity, transform);

        //Game Logo
        GameObject gameLogo = Instantiate(gameLogoPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
        gameLogo.transform.position = new Vector3(GetComponent<RectTransform>().rect.width / 2 - gameLogoMarginRight, GetComponent<RectTransform>().rect.height - gameLogo.GetComponent<RectTransform>().rect.height / 2 - gameLogoMarginTop, 0);
        gameLogo.GetComponent<RectTransform>().sizeDelta = new Vector2(gameLogoSize, gameLogoSize);

        //Boutons
        for (int i = 0; i < buttonNames.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, new Vector3(GetComponent<RectTransform>().rect.width / 2 - buttonMarginRight, GetComponent<RectTransform>().rect.height / 2, 0), Quaternion.identity, transform);
            SetButtonText(button, buttonNames[i]);
            button.transform.position += new Vector3(0, buttonMarginBottom - (i * spaceBewteenButtons), 0);
            button.GetComponent<ButtonManager>().SetButtonIndex(i);
            buttons.Add(button);
        }

        NormalizeButtonSize();
    }

    //change le text du bouton
    public void SetButtonText(GameObject button, string buttonText)
    {
        Transform child;
        for (int i = 0; i < button.transform.childCount; i++)
        {
            if (button.transform.GetChild(i).GetComponent<TextMeshProUGUI>() != null)
            {
                child = button.transform.GetChild(i);
                TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
                text.fontSize = buttonFontSize;

                text.SetText(buttonText); //set le texte
                child.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonFontSize * buttonText.Length, buttonFontSize); //adapte la boite pour être sur qu'elle soit plus grande que le texte
            }

            if (button.transform.GetChild(i).gameObject.name.Contains("BG"))
            {
                child = button.transform.GetChild(i);
                child.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonFontSize * buttonText.Length, 2 * buttonFontSize);
            }
        }

    }

    //Change la size de tous les boutons pour être de la même largeur que le plus grand bouton
    public void NormalizeButtonSize()
    {
        //Normalisation de la largeur des boutons selon le plus large.
        float maxWidth = 0;
        for (int i = 0; i < buttons.Count; i++)
        {
            Transform child;
            for (int j = 0; j < buttons[i].transform.childCount; j++)
            {
                if (buttons[i].transform.GetChild(j).gameObject.name.Contains("BG"))
                {
                    child = buttons[i].transform.GetChild(j);
                    if (child.GetComponent<RectTransform>().sizeDelta.x > maxWidth)
                    {
                        maxWidth = child.GetComponent<RectTransform>().sizeDelta.x;
                    }
                }
            }
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            Transform child;
            for (int j = 0; j < buttons[i].transform.childCount; j++)
            {
                if (buttons[i].transform.GetChild(j).gameObject.name.Contains("BG"))
                {
                    child = buttons[i].transform.GetChild(j);
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth, child.GetComponent<RectTransform>().sizeDelta.y);
                }
            }
        }
    }
    #endregion Functions

    ///////////////////////////////////////////////////////////

    #region Accessors
    #endregion Accessors
}