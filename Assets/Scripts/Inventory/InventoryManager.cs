using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will manage all the inventory related operations.
/// </summary>
public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    private Dictionary<int, ItemDetails> m_ItemDetailsDictionary;

    [SerializeField]
    private SOItemList m_ItemList = null;

    /// <summary>
    /// Create item details dictionary
    /// </summary>
    private void Start()
    {
        CreateItemDetailsDictionary();
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

}
