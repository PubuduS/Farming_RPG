using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image m_InventorySlotHighlight;
    public Image m_InventorySlotImage;
    public TextMeshProUGUI m_TextMeshProUGUI;

    [HideInInspector]
    public ItemDetails m_ItemDetails;

    [HideInInspector]
    public int m_ItemQuantity;

    [HideInInspector]
    public bool m_IsSelected = false;

    private Camera m_MainCamera;
    private Transform m_ParentItem;
    private GameObject m_DraggedItem;

    private Canvas m_ParentCanvas;

    [SerializeField]
    private UIInventoryBar m_InventoryBar;

    [SerializeField]
    private GameObject m_InventoryTextBoxPrefab = null;

    [SerializeField]
    private GameObject m_ItemPrefab = null;

    [SerializeField]
    private int m_SlotNumber = 0;

    /// <summary>
    /// Get called when the script is loaded.
    /// Initialize the parent canvas.
    /// </summary>
    private void Awake()
    {
        m_ParentCanvas = GetComponentInParent<Canvas>();
    }

    /// <summary>
    /// Unsubscribe from the AfterSceneLoadEvent event.
    /// </summary>
    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= SceneLoaded;
    }

    /// <summary>
    /// Subscribe to the AfterSceneLoadEvent event.
    /// </summary>
    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += SceneLoaded;
    }

    /// <summary>
    /// Call after the awake
    /// Initialize main camera.
    /// </summary>
    private void Start()
    {
        m_MainCamera = Camera.main;        
    }

    /// <summary>
    /// Sets this inventory slot item to be selected
    /// </summary>
    private void SetSelectedItem()
    {
        // Clear currently highlighted items.
        m_InventoryBar.ClearHighlightOnInventorySlots();

        // Highlight item on inventory bar
        m_IsSelected = true;

        // Set highlighted inventory slot.
        m_InventoryBar.SetHighlightedInventorySlots();

        // Set item selected in inventory
        InventoryManager.Instance.SetSelectedInventoryItem( InventoryLocation.Player, m_ItemDetails.m_ItemCode );

        if( m_ItemDetails.m_CanBeCarried == true )
        {
            // Show player carrying item
            Player.Instance.ShowCarriedItem( m_ItemDetails.m_ItemCode );
        }
        else
        {
            Player.Instance.ClearCarriedItem();
        }
    }

    /// <summary>
    /// Clear currently highlighted items
    /// </summary>
    private void ClearSelectedItem()
    {
        m_InventoryBar.ClearHighlightOnInventorySlots();

        m_IsSelected = false;

        // Set no item selected in inventory
        InventoryManager.Instance.ClearSelectedInventoryItem( InventoryLocation.Player );

        // Clear player carrying item
        Player.Instance.ClearCarriedItem();
    }

    /// <summary>
    /// Drop the item (if selected) at the current mouse position. Called by drop item event
    /// </summary>
    private void DropSelectedItemAtMousePosition()
    {
        if( m_ItemDetails != null && m_IsSelected )
        {
            Vector3 worldPosiiton = m_MainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, -m_MainCamera.transform.position.z ) );

            // Create item from prefab at mouse position
            GameObject itemGameObject = Instantiate( m_ItemPrefab, worldPosiiton, Quaternion.identity, m_ParentItem );
            Item item = itemGameObject.GetComponent<Item>();
            item.ItemCode = m_ItemDetails.m_ItemCode;

            // Remove item from player inventory.
            InventoryManager.Instance.RemoveItem( InventoryLocation.Player, item.ItemCode);

            // If no more of item then clear selected
            if( InventoryManager.Instance.FindItemInInventory( InventoryLocation.Player, item.ItemCode ) == -1 )
            {
                ClearSelectedItem();
            }
        }
    }

    /// <summary>
    /// Begin executing when we start to dran an item from the inventory bar
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag( PointerEventData eventData )
    {
        if( m_ItemDetails != null )
        {
            // Disable keyboard inputs.
            Player.Instance.DisablePlayerInputAndResetMovement();

            // Instantiate gameobject as dragged item
            m_DraggedItem = Instantiate( m_InventoryBar.m_InventoryBarDraggedItem, m_InventoryBar.transform );

            // Get Image for dragged item
            Image draggedItemImage = m_DraggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = m_InventorySlotImage.sprite;

            SetSelectedItem();
        }
    }

    /// <summary>
    /// Move gameobject as dragged item.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag( PointerEventData eventData )
    {
        if( m_DraggedItem != null )
        {
            m_DraggedItem.transform.position = Input.mousePosition;
        }
    }

    /// <summary>
    /// If item is droppable, drop the item otherwise destroy it.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag( PointerEventData eventData )
    {
        // Destroy gameobject as dragged item
        if( m_DraggedItem != null )
        {
            Destroy( m_DraggedItem );

            // If drag ends over inventory bar, get item drag is over and swap them
            if(  eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>() != null )
            {
                // Get the slot number when the drag ends.
                int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>().m_SlotNumber;

                // Swap inventory items in inventory list.
                InventoryManager.Instance.SwapInventoryItems( InventoryLocation.Player, m_SlotNumber, toSlotNumber );

                // Destroy inventory text box
                DestroyInventoryTextBox();

                ClearSelectedItem();
            }
            else
            {
                if( m_ItemDetails.m_CanBeDropped )
                {
                    DropSelectedItemAtMousePosition();
                }
            }

            // Enable player Input
            Player.Instance.EnablePlayerInput();
        }
    }

    /// <summary>
    /// If we click an item in the inventory set it or clear it another is already selected.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick( PointerEventData eventData )
    {
        if( eventData.button == PointerEventData.InputButton.Left )
        {
            // If inventory slot currently selected then deselect
            if( m_IsSelected == true )
            {
                ClearSelectedItem();
            }
            else
            {
                if( m_ItemQuantity > 0 )
                {
                    SetSelectedItem();
                }
            }
        }
    }

    /// <summary>
    /// Populate TextBox with item details.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter( PointerEventData eventData )
    {
        if( m_ItemQuantity != 0 )
        {
            // Instantiate inventory textbox
            m_InventoryBar.m_InventoryTextBoxGameObject = Instantiate( m_InventoryTextBoxPrefab, transform.position, Quaternion.identity );
            m_InventoryBar.m_InventoryTextBoxGameObject.transform.SetParent( m_ParentCanvas.transform, false );

            UIInventoryTextBox inventoryTextBox = m_InventoryBar.m_InventoryTextBoxGameObject.GetComponent<UIInventoryTextBox>();

            // Set item type description
            string itemTypeDescription = InventoryManager.Instance.GetItemTypeDescription( m_ItemDetails.m_ItemType );

            // Populate TextBox
            inventoryTextBox.SetTextBoxText( m_ItemDetails.m_ItemDescription, itemTypeDescription, "", m_ItemDetails.m_ItemLongDescription, "", "" );

            // Set TextBox position according to inventory bar position
            if( m_InventoryBar.IsInventoryBarPositionBottom )
            {
                m_InventoryBar.m_InventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2( 0.5f, 0f );
                m_InventoryBar.m_InventoryTextBoxGameObject.transform.position = new Vector3( transform.position.x, transform.position.y + 50f, transform.position.z );
            }
            else
            {
                m_InventoryBar.m_InventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2( 0.5f, 1f );
                m_InventoryBar.m_InventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z);
            }
        }
    }

    /// <summary>
    /// Call the function to destroy the gameobject.
    /// Once we hover away from the inventory slot, we don't need to see
    /// the item description anymore.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit( PointerEventData eventData )
    {
        DestroyInventoryTextBox();
    }

    /// <summary>
    /// Destroy gameobject when we hover away from the inventory slot.
    /// </summary>
    public void DestroyInventoryTextBox()
    {
        if( m_InventoryBar.m_InventoryTextBoxGameObject != null )
        {
            Destroy( m_InventoryBar.m_InventoryTextBoxGameObject );
        }
    }

    /// <summary>
    /// When we add a new scene, we lost the previous game object and therefore need to find it again.
    /// Otherwise, we can't throw items from inventory to ground.
    /// </summary>
    public void SceneLoaded()
    {
        m_ParentItem = GameObject.FindGameObjectWithTag( Tags.ItemsParentTransform ).transform;
    }
}
