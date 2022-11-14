using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will get the information from serialiable object and populate these class
/// Basically this will store the data in runtime.
/// </summary>
[RequireComponent(typeof(GenerateGUID))]
public class GridPropertiesManager : SingletonMonobehaviour<GridPropertiesManager>, ISaveable
{
    public Grid m_Grid;
    private Dictionary<string, GridPropertyDetails> m_GridPropertyDictionary;
    [SerializeField] private SO_GridProperties[] m_SOGridPropertiesArray = null;

    private string m_ISaveableUniqueID;

    public string ISaveableUniqueID { get { return m_ISaveableUniqueID; } set { m_ISaveableUniqueID = value; } }

    private GameObjectSave m_GameObjectSave;

    public GameObjectSave GameObjectSave { get { return m_GameObjectSave; } set { m_GameObjectSave = value; } }

    /// <summary>
    /// 
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        ISaveableUniqueID = GetComponent<GenerateGUID>().GUID;
        GameObjectSave = new GameObjectSave();
    }

    /// <summary>
    /// Call ISaveableRegister to add the iSaveableObjectList
    /// Subscribe to after scene loaded event.
    /// </summary>
    private void OnEnable()
    {
        ISaveableRegister();
        EventHandler.AfterSceneLoadEvent += AfterSceneLoaded;
    }

    /// <summary>
    /// Call ISaveableDeregister to remove the iSaveableObjectList
    /// Unsubscribe from after scene loaded event.
    /// </summary>
    private void OnDisable()
    {
        ISaveableDeregister();
        EventHandler.AfterSceneLoadEvent -= AfterSceneLoaded;
    }

    /// <summary>
    /// Call InitialiseGridProperties()
    /// </summary>
    private void Start()
    {
        InitialiseGridProperties();        
    }

    /// <summary>
    /// This initialises the grid property dictionary with the values from the SO_GridProperties 
    /// assets and stores the values for each scene in GamaObjectSave sceneData.
    /// </summary>
    private void InitialiseGridProperties()
    {
        foreach( SO_GridProperties soGridProperties in m_SOGridPropertiesArray  )
        {
            // Create dictionary of grid property details
            Dictionary<string, GridPropertyDetails> gridPropertyDictionary = new Dictionary<string, GridPropertyDetails>();

            // Populate grid property dictionaary - Iterate through all the grid properties in the so gridproperties list
            foreach( GridProperty gridProperty in soGridProperties.m_GridPropertyList )
            {
                GridPropertyDetails gridPropertyDetails;
                gridPropertyDetails = GetGridPropertyDetails( gridProperty.m_GridCoordinate.x, gridProperty.m_GridCoordinate.y, gridPropertyDictionary );

                if( gridPropertyDetails == null )
                {
                    gridPropertyDetails = new GridPropertyDetails();
                }

                switch( gridProperty.m_GridBoolProperty )
                {
                    case GridBoolProperty.Diggable:
                        gridPropertyDetails.m_IsDiggable = gridProperty.m_GridBoolValue;
                        break;

                    case GridBoolProperty.CanDropItem:
                        gridPropertyDetails.m_CanDropItem = gridProperty.m_GridBoolValue;
                        break;

                    case GridBoolProperty.CanPlaceFurniture:
                        gridPropertyDetails.m_CanPlaceFurniture = gridProperty.m_GridBoolValue;
                        break;

                    case GridBoolProperty.IsPath:
                        gridPropertyDetails.m_IsPath = gridProperty.m_GridBoolValue;
                        break;

                    case GridBoolProperty.IsNPCObstacle:
                        gridPropertyDetails.m_IsNPCObstacle = gridProperty.m_GridBoolValue;
                        break;

                    default:
                        break;

                }

                SetGridPropertyDetails( gridProperty.m_GridCoordinate.x, gridProperty.m_GridCoordinate.y, gridPropertyDetails, gridPropertyDictionary );

            }

            // Create scene save for this gameobject
            SceneSave sceneSave = new SceneSave();

            // Add grid property dictionary to scene save data.
            sceneSave.m_GridPropertyDetailsDictionary = gridPropertyDictionary;

            // If staring scene set the gridpropertyDictionary member variable to the current iteration
            if( soGridProperties.m_SceneName.ToString() == SceneControllerManager.Instance.m_StartingSceneName.ToString() )
            {
                this.m_GridPropertyDictionary = gridPropertyDictionary;
            }

            // Add scene save to game object scene data
            GameObjectSave.m_SceneData.Add( soGridProperties.m_SceneName.ToString(), sceneSave );
        }
    }

    /// <summary>
    /// Get the Grid.
    /// </summary>
    private void AfterSceneLoaded()
    {
        m_Grid = GameObject.FindObjectOfType<Grid>();
    }

    /// <summary>
    /// Return thr grid property details at the gridLocation for the supplied dictionary, or null if not found.
    /// </summary>
    /// <param name="gridX"></param>
    /// <param name="gridY"></param>
    /// <param name="gridPropertyDictionary"></param>
    /// <returns></returns>
    public GridPropertyDetails GetGridPropertyDetails( int gridX, int gridY, Dictionary<string, GridPropertyDetails> gridPropertyDictionary )
    {
        // Construct key from coordinate
        string key = "x" + gridX + "y" + gridY;

        GridPropertyDetails gridPropertyDetails;

        // Check if grid property details exist for coordinate and retrieve
        if( !gridPropertyDictionary.TryGetValue( key, out gridPropertyDetails ) )
        {
            // if not found
            return null;
        }
        else
        {
            return gridPropertyDetails;
        }
    }

    /// <summary>
    /// Get the grid property details for the tile at (gridX, gridY).
    /// If no grid property details exist null is returned and can assume that all grid property
    /// details values are null or false
    /// </summary>
    /// <returns></returns>
    public GridPropertyDetails GetGridPropertyDetails( int gridX, int gridY )
    {
        return GetGridPropertyDetails( gridX, gridY, m_GridPropertyDictionary );
    }

    /// <summary>
    /// Remove the iSaveableObjectList
    /// </summary>
    public void ISaveableDeregister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Remove( this );
    }

    /// <summary>
    /// Add the iSaveableObjectList
    /// </summary>
    public void ISaveableRegister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Add (this );
    }

    /// <summary>
    /// Restore the previous scene when we revist to that scene.
    /// Scenario: We moved scene A to scene B. In this case, we saved current condition of scene A.
    /// Then we again move scene B to scene A. Now, we need to restore the scene A as it was before we moved away.
    /// </summary>
    public void ISaveableRestoreScene( string sceneName )
    {
        // Get sceneSave for scene - it exists since we created it in initialise.
        if( GameObjectSave.m_SceneData.TryGetValue( sceneName, out SceneSave sceneSave ) )
        {
            // get grid property details dictionary - it exists since we created it in initialise
            if( sceneSave.m_GridPropertyDetailsDictionary != null )
            {
                m_GridPropertyDictionary = sceneSave.m_GridPropertyDetailsDictionary;
            }
        }
    }

    /// <summary>
    /// Saves the grid property dictionary for the current scene.
    /// This gets called when we moving scene A to scene B.
    /// In this case, we need to save current condition of the scene A, and load the scene B.
    /// This function does that.
    /// </summary>
    public void ISaveableStoreScene( string sceneName )
    {
        // Remove sceneSave for scene
        GameObjectSave.m_SceneData.Remove( sceneName );

        // Create sceneSave for scene
        SceneSave sceneSave = new SceneSave();

        // Create and add dictionary grid property details.
        sceneSave.m_GridPropertyDetailsDictionary = m_GridPropertyDictionary;

        // Add scene save to game object scene data
        GameObjectSave.m_SceneData.Add( sceneName, sceneSave );
    }

    /// <summary>
    /// Set the grid property details to gridPropertyDetails for the tile at (gridX, gridY) for current scene.
    /// </summary>
    public void SetGridPropertyDetails( int gridX, int gridY, GridPropertyDetails gridPropertyDetails )
    {
        SetGridPropertyDetails( gridX, gridY, gridPropertyDetails, m_GridPropertyDictionary );
    }

    /// <summary>
    ///  Set the grid property details to gridPropertyDetails for the tile at (gridX, gridY) for the gridPropertyDictionary.
    /// </summary>
    /// <param name="gridX"></param>
    /// <param name="gridY"></param>
    /// <param name="gridPropertyDetails"></param>
    /// <param name="gridPropertyDictionary"></param>
    public void SetGridPropertyDetails(int gridX, int gridY, GridPropertyDetails gridPropertyDetails, Dictionary<string, GridPropertyDetails> gridPropertyDictionary )
    {
        // Construct key from coordinate
        string key = "x" + gridX + "y" + gridY;

        gridPropertyDetails.m_gridX = gridX;
        gridPropertyDetails.m_gridY = gridY;

        // Set value
        gridPropertyDictionary[key] = gridPropertyDetails;
    }

}