using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will manage all the inventory related operations.
/// </summary>
public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    private Dictionary<int, ItemDetails> m_ItemDetailsDictionary;

    //! The index of the array is the inventory list, and the value is the item code.
    private int[] m_SelectedInventoryItem;

    //! We have two inventory lists, one for player and other for chest.
    //! index 0 is for player.
    public List<InventoryItem>[] m_InventoryLists;

    //! The index of the array is the inventory list (From the inventory location enum), and the value
    //! is the capacity of that inventory list.
    [HideInInspector]
    public int[] m_InventoryListCapacityIntArray;

    [SerializeField]
    private SOItemList m_ItemList = null;

    /// <summary>
    /// Create item details dictionary
    /// Keep the original awake function in Singleton class
    /// and the override it to initialize item dictionary.
    /// This will gurantee that dictionary will initialize before we used that in other classes.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        // Create Inventory List
        CreateInventoryLists();

        // Create item details dictionary
        CreateItemDetailsDictionary();

        // Initialize selected inventory item array
        m_SelectedInventoryItem = new int[(int)InventoryLocation.Count];

        for( int i = 0; i < m_SelectedInventoryItem.Length; i++ )
        {
            m_SelectedInventoryItem[i] = -1;
        }
    }

    /// <summary>
    /// Create Inventory Lists
    /// </summary>
    private void CreateInventoryLists()
    {       

        m_InventoryLists = new List<InventoryItem>[(int)InventoryLocation.Count];

        for( int i = 0; i < (int)InventoryLocation.Count; i++ )
        {
            m_InventoryLists[i] = new List<InventoryItem>();
        }

        // Initialize inventory list capacity array
        m_InventoryListCapacityIntArray = new int[(int)InventoryLocation.Count];

        // Initialize player inventory list capacity
        m_InventoryListCapacityIntArray[(int)InventoryLocation.Player] = Settings.m_PlayerInitialInventoryCapacity;
        
    }

    /// <summary>
    /// Populates the itemDetailsDictionary from scriptable object item list.
    /// </summary>
    private void CreateItemDetailsDictionary()
    {
        m_ItemDetailsDictionary = new Dictionary<int, ItemDetails>();

        foreach( ItemDetails itemDetails in m_ItemList.m_ItemDetails )
        {
            m_ItemDetailsDictionary.Add( itemDetails.m_ItemCode, itemDetails );
        }
    }

    /// <summary>
    /// Add an item to the inventory List for the inventory location
    /// </summary>
    /// <param name="inventoryLocation"></param>
    /// <param name="item"></param>
    public void AddItem( InventoryLocation inventoryLocation, Item item )
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = m_InventoryLists[(int)inventoryLocation];

        // Check if inventory already contains the item
        int itemPosition = FindItemInInventory( inventoryLocation, itemCode );

        if( itemPosition != -1 )
        {
            AddItemAtPosition( inventoryList, itemCode, itemPosition );
        }
        else 
        {
            AddItemAtPosition( inventoryList, itemCode );
        }

        // Send event that inventory has been updated
        EventHandler.CallInventoryUpdatedEvent( inventoryLocation, m_InventoryLists[(int)inventoryLocation] );
    }

    /// <summary>
    /// Add an item to the inventory list for the inventoryLocation and then destroy the gameObjectToDelete
    /// </summary>
    /// <param name="inventoryLocation"></param>
    /// <param name="item"></param>
    /// <param name="gameObjectToDelete"></param>
    public void AddItem( InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDelete )
    {
        AddItem( inventoryLocation, item );
        Destroy( gameObjectToDelete );
    }

    /// <summary>
    /// Add item to the end of the inventory.
    /// </summary>
    /// <param name="inventoryList"></param>
    /// <param name="itemCode"></param>
    private void AddItemAtPosition( List<InventoryItem> inventoryList, int itemCode )
    {
        InventoryItem inventoryItem = new InventoryItem();

        inventoryItem._ItemCode = itemCode;
        inventoryItem._ItemQuantity = 1;
        inventoryList.Add( inventoryItem );

        // DebugPrintInventoryList( inventoryList );
    }

    /// <summary>
    /// Add item to postion in the inventory
    /// </summary>
    private void AddItemAtPosition( List<InventoryItem> inventoryList, int itemCode, int position )
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position]._ItemQuantity + 1;
        inventoryItem._ItemCode = itemCode;
        inventoryItem._ItemQuantity = quantity;
        inventoryList[position] = inventoryItem;

        Debug.ClearDeveloperConsole();
        // DebugPrintInventoryList( inventoryList );
    }

    /// <summary>
    /// Swap item at fromItem index with item at toItem index in inventoryLocation inventory list.
    /// </summary>
    /// <param name="inventoryLocation"></param>
    /// <param name="fromItem"></param>
    /// <param name="toItem"></param>
    public void SwapInventoryItems( InventoryLocation inventoryLocation, int fromItem, int toItem )
    {
        // If fromItem index and toItemIndex are within the bounds of the list, not the same, and greater than or equal to zero
        if( fromItem < m_InventoryLists[(int)inventoryLocation].Count && toItem < m_InventoryLists[(int)inventoryLocation].Count 
            && fromItem != toItem && fromItem >= 0 && toItem >= 0 )
        {
            InventoryItem fromInventoryItem = m_InventoryLists[(int)inventoryLocation][fromItem];
            InventoryItem toInventoryItem = m_InventoryLists[(int)inventoryLocation][toItem];

            m_InventoryLists[(int)inventoryLocation][toItem] = fromInventoryItem;
            m_InventoryLists[(int)inventoryLocation][fromItem] = toInventoryItem;

            // Send event that inventory has been updated.
            EventHandler.CallInventoryUpdatedEvent( inventoryLocation, m_InventoryLists[(int)inventoryLocation] );
        }
    }

    /// <summary>
    /// Clear the selected inventory item for inventory Location.
    /// </summary>
    public void ClearSelectedInventoryItem( InventoryLocation inventoryLocation )
    {
        m_SelectedInventoryItem[(int)inventoryLocation] = -1;
    }

    /// <summary>
    /// Find if an itemCode is already in the inventory. Returns the item position
    /// in the inventory list, or -1 if the item is not in the inventory
    /// </summary>
    /// <param name="inventoryLocation"></param>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    public int FindItemInInventory( InventoryLocation inventoryLocation, int itemCode )
    {
        List<InventoryItem> inventoryList = m_InventoryLists[(int)inventoryLocation];

        for( int i = 0; i < inventoryList.Count; i++ )
        {
            if( inventoryList[i]._ItemCode == itemCode )
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Returns the itemDetails (from the SOItemList) for the itemCode, or null if the item code doesn't exist.
    /// </summary>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    public ItemDetails GetItemDetails( int itemCode )
    {
        ItemDetails itemDetails;

        if( m_ItemDetailsDictionary.TryGetValue( itemCode, out itemDetails) )
        {
            return itemDetails;
        }

        return null;
    }

    /// <summary>
    /// Return the desctiption of the item type.
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public string GetItemTypeDescription( ItemType itemType )
    {
        string itemTypeDescription;

        switch( itemType )
        {
            case ItemType.BreakingTool:
                itemTypeDescription = Settings.m_BreakingTool;
                break;

            case ItemType.ChoppingTool:
                itemTypeDescription = Settings.m_ChoppingTool;
                break;

            case ItemType.HoeingTool:
                itemTypeDescription = Settings.m_HoeingTool;
                break;

            case ItemType.ReapingTool:
                itemTypeDescription = Settings.m_ReapingTool;
                break;

            case ItemType.WateringTool:
                itemTypeDescription = Settings.m_WateringTool;
                break;

            case ItemType.CollectingTool:
                itemTypeDescription = Settings.m_CollectingTool;
                break;

            default:
                itemTypeDescription = itemType.ToString();
                break;
        }

        return itemTypeDescription;
    }

    /// <summary>
    /// Remove an item from the inventory, and create a gameobject at the position it was dropped
    /// </summary>
    public void RemoveItem( InventoryLocation inventoryLocation, int itemCode )
    {
        List<InventoryItem> inventoryList = m_InventoryLists[(int)inventoryLocation];

        // Check if inventory already contains the item.
        int itemPosition = FindItemInInventory( inventoryLocation, itemCode );

        if( itemPosition != -1 )
        {
            RemoveItemAtPosition( inventoryList, itemCode, itemPosition );
        }

        // Send event that inventory has been updated
        EventHandler.CallInventoryUpdatedEvent( inventoryLocation, m_InventoryLists[(int)inventoryLocation] );
    }

    /// <summary>
    /// Remove an item from a specific position
    /// </summary>
    /// <param name="inventoryList"></param>
    /// <param name="itemCode"></param>
    /// <param name="position"></param>
    private void RemoveItemAtPosition( List<InventoryItem> inventoryList, int itemCode, int position )
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position]._ItemQuantity - 1;

        if( quantity > 0 )
        {
            inventoryItem._ItemQuantity = quantity;
            inventoryItem._ItemCode = itemCode;
            inventoryList[position] = inventoryItem;
        }
        else
        {
            inventoryList.RemoveAt(position);
        }
    }

    /// <summary>
    /// Set the selected inventory item for inventoryLocation to itemCode
    /// </summary>
    /// <param name="inventoryLocation"></param>
    /// <param name="itemCode"></param>
    public void SetSelectedInventoryItem( InventoryLocation inventoryLocation, int itemCode )
    {
        m_SelectedInventoryItem[(int)inventoryLocation] = itemCode;
    }

    /// <summary>
    /// 
    /// </summary>
    //private void DebugPrintInventoryList( List<InventoryItem> inventoryList )
    //{
    //    foreach( InventoryItem inventoryItem in inventoryList )
    //    {
    //        Debug.Log("Item Description: " + InventoryManager.Instance.GetItemDetails( inventoryItem._ItemCode ).m_ItemDescription + "  Item Quantity: " + inventoryItem._ItemQuantity );
    //    }
    //    Debug.Log("********************************************************");
    //}

}
