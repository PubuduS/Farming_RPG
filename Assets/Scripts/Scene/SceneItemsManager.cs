using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control store, restore, save and load functionality in the scene.
/// </summary>

[RequireComponent(typeof(GenerateGUID))]
public class SceneItemsManager : SingletonMonobehaviour<SceneItemsManager>, ISaveable
{
    private Transform m_ParentItem;

    [SerializeField] private GameObject m_ItemPrefab = null;

    private string iSaveableUniqueID;

    private GameObjectSave m_GameObjectSave;

    public string ISaveableUniqueID { get => iSaveableUniqueID; set => iSaveableUniqueID = value; }
    public GameObjectSave GameObjectSave { get => m_GameObjectSave; set => m_GameObjectSave = value; }

    /// <summary>
    /// Find the game object of the parent
    /// </summary>
    private void AfterSceneLoad()
    {
        m_ParentItem = GameObject.FindGameObjectWithTag( Tags.ItemsParentTransform ).transform;
    }

    /// <summary>
    /// Call the base of singleton class
    /// Initialize variables.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        ISaveableUniqueID = GetComponent<GenerateGUID>().GUID;
        GameObjectSave = new GameObjectSave();
    }

    /// <summary>
    /// Destroy Items currently in the scene
    /// </summary>
    private void DestroySceneItems()
    {
        // Get all items in the scene
        Item[] itemsInScene = GameObject.FindObjectsOfType<Item>();

        // Loop through all scene items and destroy them
        for( int i = itemsInScene.Length - 1; i > -1; i--)
        {
            Destroy( itemsInScene[i].gameObject );
        }
    }

    /// <summary>
    /// For future.
    /// Use to instantiate a single SceneItem
    /// </summary>
    public void InstantiateSceneItem( int itemCode, Vector3 itemPosition )
    {
        GameObject itemGameObject = Instantiate( m_ItemPrefab, itemPosition, Quaternion.identity, m_ParentItem );
        Item item = itemGameObject.GetComponent<Item>();
        item.Init( itemCode );
    }

    /// <summary>
    /// Loop through all the scene items
    /// Then instantiate a game object for each SceneItem
    /// </summary>
    private void InstantiateSceneItems( List<SceneItem> sceneItemList )
    {
        GameObject itemGameObject;

        foreach( SceneItem sceneItem in sceneItemList )
        {
            itemGameObject = Instantiate( m_ItemPrefab, new Vector3( sceneItem.m_Position.m_X, sceneItem.m_Position.m_Y, sceneItem.m_Position.m_Z ), Quaternion.identity, m_ParentItem );

            Item item = itemGameObject.GetComponent<Item>();
            item.ItemCode = sceneItem.m_ItemCode;
            item.name = sceneItem.m_ItemName;
        }
    }

    /// <summary>
    /// Calls ISaveableDeregister
    /// Unsubscribe from AfterSceneLoad
    /// </summary>
    private void OnDisable()
    {
        ISaveableDeregister();
        EventHandler.AfterSceneLoadEvent -= AfterSceneLoad;
    }

    /// <summary>
    /// Calls ISaveableRegister
    /// Subscribe to AfterSceneLoad
    /// </summary>
    private void OnEnable()
    {
        ISaveableRegister();
        EventHandler.AfterSceneLoadEvent += AfterSceneLoad;
    }

    /// <summary>
    /// Removing the current game object to iSaveableObjectList instance in SaveLoadManager
    /// </summary>
    public void ISaveableDeregister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Remove( this );
    }

    /// <summary>
    /// Adding the current game object to iSaveableObjectList instance in SaveLoadManager
    /// </summary>
    public void ISaveableRegister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Add( this );
    }

    /// <summary>
    /// Restore the scene
    /// </summary>
    /// <param name="sceneName"></param>
    public void ISaveableRestoreScene( string sceneName )
    {
        if( GameObjectSave.m_SceneData.TryGetValue( sceneName, out SceneSave sceneSave ) )
        {
            if( sceneSave.m_ListSceneItemDictionary != null && sceneSave.m_ListSceneItemDictionary.TryGetValue("sceneItemList", out List<SceneItem> sceneItemList) )
            {
                // Scene list item found - destroy existing items in scene
                DestroySceneItems();

                // Now instantiate the list of scene items
                InstantiateSceneItems( sceneItemList );
            }
        }
    }

    /// <summary>
    /// Remove old scene data
    /// Save all the information on new scene
    /// Add that back to the GameObjectSave
    /// </summary>
    /// <param name="sceneName"></param>
    public void ISaveableStoreScene( string sceneName )
    {
        // Remove old scene save for gameObject if exists
        GameObjectSave.m_SceneData.Remove( sceneName );

        // Get all items in the scene
        List<SceneItem> sceneItemList = new List<SceneItem>();
        Item[] itemsInScene = FindObjectsOfType<Item>();

        // Loop through all scene items
        foreach( Item item in itemsInScene )
        {
            SceneItem sceneItem = new SceneItem();
            sceneItem.m_ItemCode = item.ItemCode;
            sceneItem.m_Position = new Vector3Serializable( item.transform.position.x, item.transform.position.y, item.transform.position.z );
            sceneItem.m_ItemName = item.name;

            // Add scene item to list
            sceneItemList.Add( sceneItem );
        }

        // Create list scene items dictionary in scene save and add to it
        SceneSave sceneSave = new SceneSave();
        sceneSave.m_ListSceneItemDictionary = new Dictionary<string, List<SceneItem>>();
        sceneSave.m_ListSceneItemDictionary.Add( "sceneItemList", sceneItemList );

        // Add scene save to game object
        GameObjectSave.m_SceneData.Add( sceneName, sceneSave );
    }

}


