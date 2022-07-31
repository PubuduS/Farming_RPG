using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage saving and loading scenes.
/// This class is a singleton instance.
/// </summary>
public class SaveLoadManager : SingletonMonobehaviour<SaveLoadManager>
{
    public List<ISaveable> iSaveableObjectList;

    protected override void Awake()
    {
        base.Awake();
        iSaveableObjectList = new List<ISaveable>();
    }

    /// <summary>
    /// Loop through all ISaveable objects and trigger store scene data for each
    /// </summary>
    public void StoreCurrentSceneData()
    {        
        foreach( ISaveable iSaveableObject in iSaveableObjectList )
        {
            iSaveableObject.ISaveableStoreScene( SceneManager.GetActiveScene().name );
        }
    }

    /// <summary>
    /// Loop through all ISaveable objects and trigger restore scene data for each
    /// </summary>
    public void RestoreCurrentSceneData()
    {        
        foreach( ISaveable iSaveableObject in iSaveableObjectList )
        {
            iSaveableObject.ISaveableRestoreScene( SceneManager.GetActiveScene().name );
        }
    }
}