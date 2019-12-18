#pragma warning disable CS0414
#region Author
/////////////////////////////////////////
//   RAPHAËL DAUMAS --> BackgroundGenerator
//   https://raphdaumas.wixsite.com/portfolio
//   https://github.com/Marsgames
/////////////////////////////////////////
#endregion
using UnityEngine;

[CreateAssetMenu(menuName = "MenuGenerator/BackgroundAspect")]
public class BackgroundGenerator : ScriptableObject
{
    #region Variables
    [SerializeField] private Sprite m_sourceImage = null;
    [SerializeField] private Color m_color = new Color(1, 1, 1);
    #endregion Variables

    ///////////////////////////////////////////////////////////

    #region Unity's functions
    #endregion Unity's functions

    ///////////////////////////////////////////////////////////

    #region Functions
    #endregion Functions

    ///////////////////////////////////////////////////////////

    #region Accessors
    public Sprite GetImage()
    {
        return m_sourceImage;
    }

    public Color GetColor()
    {
        return m_color;
    }
    #endregion Accessors
}