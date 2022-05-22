using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will manage all the inventory related operations.
/// </summary>
public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    private Dictionary<int, ItemDetails> m_ItemDetailsDictionary;

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
