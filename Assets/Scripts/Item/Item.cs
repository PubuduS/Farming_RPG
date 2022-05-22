using UnityEngine;

public class Item : MonoBehaviour
{
    [ItemCodeDescription]
    [SerializeField]
    private int m_ItemCode;

    private SpriteRenderer m_SpriteRenderer;

    public int ItemCode { get => m_ItemCode; set => m_ItemCode = value; }

    private void Awake()
    {
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if( ItemCode != 0 )
        {
            Init( ItemCode );
        }
    }

    /// <summary>
    /// At the start if we have a reapable item,
    /// we add the item nudge class.
    /// </summary>
    /// <param name="itemCode"></param>
    public void Init( int itemCode )
    {
        if( itemCode != 0 )
        {
            ItemCode = itemCode;

            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails( ItemCode );

            m_SpriteRenderer.sprite = itemDetails.m_ItemSprite;

            // If item type is reapable then add nudgeable component
            if( itemDetails.m_ItemType == ItemType.ReapableScenary )
            {
                gameObject.AddComponent<ItemNudge>();
            }
        }
    }
}
