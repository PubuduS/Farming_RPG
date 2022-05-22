using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    /// <summary>
    /// Changed the return property height to be double to cater for the additional item code description that we we draw.
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
    {
        return EditorGUI.GetPropertyHeight( property ) * 2;
    }

    /// <summary>
    /// Using BeginProperty / EndProperty on the parent property means that prefab ovrride logic works on the entire property
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
    {
        EditorGUI.BeginProperty( position, label, property );

        if( property.propertyType == SerializedPropertyType.Integer )
        {
            // Start of check for changed values.
            EditorGUI.BeginChangeCheck();

            // Draw item code
            var newValue = EditorGUI.IntField( new Rect( position.x, position.y, position.width, position.height / 2 ), label, property.intValue );

            // Draw item description
            EditorGUI.LabelField(  new Rect( position.x, position.y + position.height / 2, position.width, position.height / 2 ), "Item Description", GetItemDescription( property.intValue ) );

            // If item code value has changed, the set value to new value.
            if( EditorGUI.EndChangeCheck() )
            {
                property.intValue = newValue;
            }
        }

        EditorGUI.EndProperty();
    }

    /// <summary>
    /// Get the description of the item.
    /// </summary>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    private string GetItemDescription( int itemCode )
    {
        SOItemList soItemList;

        soItemList = AssetDatabase.LoadAssetAtPath( "Assets/Scriptable Objects Assets/Item/soItemList.asset", typeof( SOItemList ) ) as SOItemList;

        List<ItemDetails> itemDetailsList = soItemList.m_ItemDetails;

        ItemDetails itemDetail = itemDetailsList.Find( x=>x.m_ItemCode == itemCode );

        if( itemDetail != null )
        {
            return itemDetail.m_ItemDescription;
        }

        return "";

    }
}
