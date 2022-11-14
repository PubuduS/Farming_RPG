using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
/// <summary>
/// This script by default will execute on both editor and runtime.
/// </summary>
public class TilemapGridProperties : MonoBehaviour
{
    private Tilemap m_Tilemap;    
    [SerializeField] private SO_GridProperties m_GridProperties = null;
    [SerializeField] private GridBoolProperty m_GridBoolProperty = GridBoolProperty.Diggable;

    /// <summary>
    /// Only populate in the editor time.
    /// Clear the grid property list allowing it to store new values.
    /// </summary>
    private void OnEnable()
    {
        if( !Application.IsPlaying( gameObject ) )
        {
            m_Tilemap = GetComponent<Tilemap>();

            if( m_GridProperties != null )
            {
                m_GridProperties.m_GridPropertyList.Clear();
            }
        }
    }

    /// <summary>
    /// Only populate in the editor time.
    /// Upon disable, it will capture all the details of tilemap and save it to scriptable objects.
    /// This is required to ensure that the updated grid properties gameobject gets saved when the game is saved - otherwise they are not saved.
    /// </summary>
    private void OnDisable()
    {
        if( !Application.IsPlaying( gameObject ) )
        {
            UpdateGridProperties();

            if( m_GridProperties != null )
            {
                EditorUtility.SetDirty(m_GridProperties);
            }
        }
    }

    /// <summary>
    /// Goes through the tilemap and captures everything on there.
    /// </summary>
    private void UpdateGridProperties()
    {
        // Compress Tilemap Bounds.
        m_Tilemap.CompressBounds();

        if( !Application.IsPlaying( gameObject ) )
        {
            if( m_GridProperties != null )
            {
                Vector3Int startCell = m_Tilemap.cellBounds.min;
                Vector3Int endCell = m_Tilemap.cellBounds.max;

                for( int x = startCell.x; x < endCell.x; x++ )
                {
                    for( int y = startCell.y; y < endCell.y; y++ )
                    {
                        TileBase tile = m_Tilemap.GetTile( new Vector3Int( x, y, 0) );

                        if( tile != null )
                        {
                            m_GridProperties.m_GridPropertyList.Add( new GridProperty( new GridCoordinate(x, y), m_GridBoolProperty, true) );
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {        
        if( !Application.IsPlaying( gameObject ) )
        {
            Debug.Log("DISABLE PROPERTY TILEMAPS");
        }
    }
}
