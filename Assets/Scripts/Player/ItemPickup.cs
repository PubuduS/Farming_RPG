using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    /// <summary>
    /// Get item details from collided object if the item details class attached to that.
    /// Print the details for testing purposes.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        if( item != null )
        {
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails( item.ItemCode );

            // If item can be picked up
            if( itemDetails.m_CanPickedUp == true )
            {
                // Add item to the inventory
                InventoryManager.Instance.AddItem( InventoryLocation.Player, item, collision.gameObject );
            }
        }
    }
}
