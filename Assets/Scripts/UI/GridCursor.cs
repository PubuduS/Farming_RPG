using UnityEngine;
using UnityEngine.UI;

public class GridCursor : MonoBehaviour
{
    private Canvas m_Canvas;
    private Grid m_Grid;
    private Camera m_MainCamera;

    [SerializeField] private Image m_CursorImage = null;
    [SerializeField] private RectTransform m_CursorRectTransform = null;
    [SerializeField] private Sprite m_GreenCursorSprite = null;
    [SerializeField] private Sprite m_RedCursorSprite = null;

    private bool m_CursorPositionIsValid = false;
    private int m_ItemUseGridRadius = 0;
    private ItemType m_SelectedItemType;
    private bool m_CursorIsEnabled = false;

    public bool CursorPositionIsValid { get => m_CursorPositionIsValid; set => m_CursorPositionIsValid = value; }
    public int ItemUseGridRadius { get => m_ItemUseGridRadius; set => m_ItemUseGridRadius = value; }
    public ItemType SelectedItemType { get => m_SelectedItemType; set => m_SelectedItemType = value; }
    public bool CursorIsEnabled { get => m_CursorIsEnabled; set => m_CursorIsEnabled = value; }

    /// <summary>
    /// Unsubscribe sceneload
    /// </summary>
    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= SceneLoaded;
    }

    /// <summary>
    /// Subscribe scene load.
    /// </summary>
    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += SceneLoaded;
    }

    /// <summary>
    /// Initialize camera and canvas
    /// </summary>
    private void Start()
    {
        m_MainCamera = Camera.main;
        m_Canvas = GetComponentInParent<Canvas>();
    }

    /// <summary>
    /// Call display cursor function at every frame.
    /// </summary>
    private void Update()
    {
        if( CursorIsEnabled )
        {
            DisplayCursor();
        }
    }

    /// <summary>
    /// Display the cursor which marked valid and invalid drop locations.
    /// </summary>
    /// <returns></returns>
    private Vector3Int DisplayCursor()
    {
        if( m_Grid != null )
        {
            // Get grid position for the cursor.
            Vector3Int gridPosition = GetGridPositionForCursor();

            // Get grid position for player.
            Vector3Int playerGridPosition = GetGridPositionForPlayer();

            // Set cursor sprite.
            SetCursorValidity( gridPosition, playerGridPosition );

            // Get rect transform position for cursor.
            m_CursorRectTransform.position = GetRectTransformPositionForCursor( gridPosition );

            return gridPosition;
        }
        else
        {
            return Vector3Int.zero;
        }
    }

    /// <summary>
    /// Initialize the Grid
    /// </summary>
    private void SceneLoaded()
    {
        m_Grid = GameObject.FindObjectOfType<Grid>();
    }

    /// <summary>
    /// Check series of checks to determine validity of the cursor location.
    /// Based on the validity set the cursor to be valid or invalid.
    /// </summary>
    /// <param name="cursorGridPosition"></param>
    /// <param name="playerGridPosition"></param>
    private void SetCursorValidity( Vector3Int cursorGridPosition, Vector3Int playerGridPosition )
    {
        SetCursorToValid();

        // Check item user radius is valid.
        if( ( Mathf.Abs( cursorGridPosition.x - playerGridPosition.x ) > ItemUseGridRadius ) ||
            ( Mathf.Abs( cursorGridPosition.y - playerGridPosition.y ) > ItemUseGridRadius ) )
        {
            SetCursorToInvalid();
            return;
        }

        // Get selected item details.
        ItemDetails itemDetails = InventoryManager.Instance.GetSelectedInventoryItemDetails( InventoryLocation.Player );

        if( itemDetails == null )
        {
            SetCursorToInvalid();
            return;
        }

        // Get grid property details at cursor position.
        GridPropertyDetails gridPropertyDetails = GridPropertiesManager.Instance.GetGridPropertyDetails( cursorGridPosition.x, cursorGridPosition.y );

        if( gridPropertyDetails != null )
        {
            // Determine cursor validity based on inventory item selected and grid property details.
            switch( itemDetails.m_ItemType )
            {
                case ItemType.Seed:
                    if( !IsCursorValidForSeed( gridPropertyDetails ) )
                    {
                        SetCursorToInvalid();
                        return;
                    }
                    break;

                case ItemType.Commodity:
                    if( !IsCursorValidForCommodity( gridPropertyDetails ) )
                    {
                        SetCursorToInvalid();
                        return;
                    }
                    break;

                case ItemType.None:
                    break;

                case ItemType.Count:
                    break;

                default:
                    break;
            }
        }
        else
        {
            SetCursorToInvalid();
            return;
        }
    }

    /// <summary>
    /// Set the cursor to valid.
    /// </summary>
    private void SetCursorToValid()
    {
        m_CursorImage.sprite = m_GreenCursorSprite;
        CursorPositionIsValid = true;
    }

    /// <summary>
    /// Set the cursor to invalid.
    /// </summary>
    private void SetCursorToInvalid()
    {
        m_CursorImage.sprite = m_RedCursorSprite;
        CursorPositionIsValid = false;
    }

    /// <summary>
    /// Test cursor validity for a commodity for the target gridPropertyDetails. 
    /// </summary>
    /// <param name="gridPropertyDetails"></param>
    /// <returns>Return true if valid otherwise false. </returns>
    private bool IsCursorValidForCommodity( GridPropertyDetails gridPropertyDetails )
    {
        return gridPropertyDetails.m_CanDropItem;
    }

    /// <summary>
    /// Test cursor validity for a seed for the target gridPropertyDetails. 
    /// </summary>
    /// <returns>Return true if valid otherwise false.</returns>
    private bool IsCursorValidForSeed( GridPropertyDetails gridPropertyDetails )
    {
        return gridPropertyDetails.m_CanDropItem;
    }

    /// <summary>
    /// Disable the cursor.
    /// </summary>
    public void DisableCursor()
    {
        m_CursorImage.color = Color.clear;
        CursorIsEnabled = false;
    }

    /// <summary>
    /// Enable the cursor.
    /// </summary>
    public void EnableCursor()
    {
        m_CursorImage.color = new Color( 1f, 1f, 1f, 1f );
        CursorIsEnabled = true;
    }

    /// <summary>
    /// Return the position of the cursor.
    /// </summary>
    /// <returns></returns>
    public Vector3Int GetGridPositionForCursor()
    {
        // Z is how far the objects infront of the camera.
        Vector3 worldPosition = m_MainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, -m_MainCamera.transform.position.z ) );
        return m_Grid.WorldToCell( worldPosition );
    }

    /// <summary>
    /// Return the grid position of the player.
    /// </summary>
    /// <returns></returns>
    public Vector3Int GetGridPositionForPlayer()
    {
        return m_Grid.WorldToCell( Player.Instance.transform.position );
    }

    /// <summary>
    /// Transfor the UI Items on canvas to pixel positions.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetRectTransformPositionForCursor( Vector3Int gridPosition )
    {
        Vector3 gridWorldPosition = m_Grid.CellToWorld( gridPosition );
        Vector2 gridScreenPosition = m_MainCamera.WorldToScreenPoint( gridWorldPosition );
        return RectTransformUtility.PixelAdjustPoint( gridScreenPosition, m_CursorRectTransform, m_Canvas );
    }

}
