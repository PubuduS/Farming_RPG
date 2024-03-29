﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryBar : MonoBehaviour
{

    [SerializeField]
    private Sprite m_Blank16x16Sprite = null;

    [SerializeField]
    private UIInventorySlot[] m_InventorySlot = null;

    public GameObject m_InventoryBarDraggedItem;

    [HideInInspector]
    public GameObject m_InventoryTextBoxGameObject;

    private RectTransform m_RectTransform;

    private bool m_IsInventoryBarPositionBottom = true;

    public bool IsInventoryBarPositionBottom { get => m_IsInventoryBarPositionBottom; set => m_IsInventoryBarPositionBottom = value; }

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Subscribed to InventoryUpdatedEvent to trigger InventoryUpdated method.
    /// </summary>
    private void OnEnable()
    {
        EventHandler.m_InventoryUpdatedEvent += InventoryUpdated;
    }

    /// <summary>
    /// Unsubscribed to InventoryUpdatedEvent to trigger InventoryUpdated method.
    /// </summary>
    private void OnDisable()
    {
        EventHandler.m_InventoryUpdatedEvent -= InventoryUpdated;
    }

    /// <summary>
    /// Switch inventory bar position depending on player position
    /// </summary>
    private void Update()
    {
        SwitchInventoryBarPosition();
    }

    /// <summary>
    /// Clear all highlights from the inventory bar.
    /// </summary>
    public void ClearHighlightOnInventorySlots()
    {
        if( m_InventorySlot.Length > 0 )
        {
            // Loop through inventory slots and clear highlight sprites
            for( int i = 0; i < m_InventorySlot.Length; i++ )
            {
                if( m_InventorySlot[i].m_IsSelected )
                {
                    m_InventorySlot[i].m_IsSelected = false;
                    m_InventorySlot[i].m_InventorySlotHighlight.color = new Color( 0f, 0f, 0f, 0f );

                    // Update inventory to  show item as not selected
                    InventoryManager.Instance.ClearSelectedInventoryItem( InventoryLocation.Player );
                }
            }
        }
    }

    /// <summary>
    /// Clear the inventory slots to default values.
    /// </summary>
    private void ClearInventorySlots()
    {
        if( m_InventorySlot.Length > 0 )
        {
            for( int i = 0; i < m_InventorySlot.Length; i++ )
            {
                m_InventorySlot[i].m_InventorySlotImage.sprite = m_Blank16x16Sprite;
                m_InventorySlot[i].m_TextMeshProUGUI.text = "";
                m_InventorySlot[i].m_ItemDetails = null;
                m_InventorySlot[i].m_ItemQuantity = 0;

                SetHighlightedInventorySlots(i);
            }
        }
    }

    /// <summary>
    /// Draw the images of new items in the inventory.
    /// </summary>
    /// <param name="inventoryLocation"></param>
    /// <param name="inventoryList"></param>
    private void InventoryUpdated( InventoryLocation inventoryLocation, List<InventoryItem> inventoryList )
    {
        if( inventoryLocation == InventoryLocation.Player )
        {
            ClearInventorySlots();

            if( m_InventorySlot.Length > 0 && inventoryList.Count > 0 )
            {
                // Loop through inventory slots and update with corresponding inventory list item.
                for( int i = 0; i < m_InventorySlot.Length; i++  )
                {
                    if( i < inventoryList.Count )
                    {
                        int itemCode = inventoryList[i]._ItemCode;

                        // ItemDetails itemDetails = InventoryManager.Instance.m_ItemList.itemDetails.Find( x=> x.itemCode == itemCode );
                        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails( itemCode );

                        if( itemDetails != null )
                        {
                            // Add images and details to inventory item slot
                            m_InventorySlot[i].m_InventorySlotImage.sprite = itemDetails.m_ItemSprite;
                            m_InventorySlot[i].m_TextMeshProUGUI.text = inventoryList[i]._ItemQuantity.ToString();
                            m_InventorySlot[i].m_ItemDetails = itemDetails;
                            m_InventorySlot[i].m_ItemQuantity = inventoryList[i]._ItemQuantity;

                            SetHighlightedInventorySlots(i);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set the selected highlight if set on all inventory item position
    /// </summary>
    public void SetHighlightedInventorySlots()
    {
        if( m_InventorySlot.Length > 0 )
        {
            // Loop through inventory slots and clear highlight sprites
            for( int i = 0; i < m_InventorySlot.Length; i++ )
            {
                SetHighlightedInventorySlots(i);
            }
        }
    }

    /// <summary>
    /// Set the selected highlight if set on an inventory item for a given slot item position
    /// </summary>
    public void SetHighlightedInventorySlots( int itemPosition )
    {
        if( m_InventorySlot.Length > 0 && m_InventorySlot[itemPosition].m_ItemDetails != null )
        {
            if( m_InventorySlot[itemPosition].m_IsSelected )
            {
                m_InventorySlot[itemPosition].m_InventorySlotHighlight.color = new Color( 1f, 1f, 1f, 1f );

                // Update inventory to show item as selected
                InventoryManager.Instance.SetSelectedInventoryItem( InventoryLocation.Player, m_InventorySlot[itemPosition].m_ItemDetails.m_ItemCode );
            }
        }
    }

    /// <summary>
    /// When player move to the bottom we switch our inventory to the top to avoid player been obscured.
    /// </summary>
    private void SwitchInventoryBarPosition()
    {
        Vector3 playerViewportPosition = Player.Instance.GetPlayerViewportPosition();

        if( playerViewportPosition.y > 0.3f && IsInventoryBarPositionBottom == false )
        {
            // tranform.position = new Vector3( transform.position.x, 7.5f, 0f ); // This was changed to control the rect transform see below
            m_RectTransform.pivot = new Vector2( 0.5f, 0f );
            m_RectTransform.anchorMin = new Vector2( 0.5f, 0f );
            m_RectTransform.anchorMax = new Vector2( 0.5f, 0f );
            m_RectTransform.anchoredPosition = new Vector2( 0f, 2.5f );

            IsInventoryBarPositionBottom = true;
        }
        else if( playerViewportPosition.y <= 0.3f && IsInventoryBarPositionBottom == true )
        {
            // tranform.position = new Vector3( transform.position.x, m_MainCamera.pixelHeight - 120f, 0f ); // This was changed to control the rect transform see below
            m_RectTransform.pivot = new Vector2(0.5f, 1f);
            m_RectTransform.anchorMin = new Vector2(0.5f, 1f);
            m_RectTransform.anchorMax = new Vector2(0.5f, 1f);
            m_RectTransform.anchoredPosition = new Vector2(0f, -2.5f);

            IsInventoryBarPositionBottom = false;

        }

    }
}
