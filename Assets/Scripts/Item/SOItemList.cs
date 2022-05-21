using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "soItemList", menuName = "Scriptable Objects/Item/Item List")]
public class SOItemList : ScriptableObject
{
    [SerializeField]
    public List<ItemDetails> m_ItemDetails;


}
