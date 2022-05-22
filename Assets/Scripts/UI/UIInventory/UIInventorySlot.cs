using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public Image m_InventorySlotHighlight;
    public Image m_InventorySlotImage;
    public TextMeshProUGUI m_TextMeshProUGUI;

    [HideInInspector]
    public ItemDetails m_ItemDetails;

    [HideInInspector]
    public int m_ItemQuantity;


}
