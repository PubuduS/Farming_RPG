using UnityEngine;

/// <summary>
/// This class holds information of items.
/// </summary>
[System.Serializable]
public class ItemDetails
{
    public string m_ItemDescription;
    public string m_ItemLongDescription;

    public Sprite m_ItemSprite;  
    
    public short m_ItemUseGridRadius;
    public float m_ItemUseRadius;
    public int m_ItemCode;
    public ItemType m_ItemType;

    public bool m_IsStartingItem;
    public bool m_CanPickedUp;
    public bool m_CanBeDropped;
    public bool m_CanBeEaten;
    public bool m_CanBeCarried;
}
