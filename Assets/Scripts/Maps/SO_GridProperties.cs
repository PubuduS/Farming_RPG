using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "so_GridProperties", menuName = "Scriptable Objects/Grid Properties" )]
/// <summary>
/// This ScriptableObject object asset will contain all the information of each scene.
/// </summary>
public class SO_GridProperties : ScriptableObject
{
    /// Scene name
    public SceneName m_SceneName;

    /// Tile map may vary in sizes. Therefore, this defins the width.
    public int m_GridWidth;

    /// Tile map may vary in sizes. Therefore, this defins the height.
    public int m_GridHeight;

    /// Always describes bottom left corner.
    public int m_OriginX;
    public int m_OriginY;

    [SerializeField]
    /// For every grid, we read our tilemap and add to this list.
    public List<GridProperty> m_GridPropertyList;
}
