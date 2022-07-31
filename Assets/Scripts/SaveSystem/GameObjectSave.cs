using System.Collections.Generic;

/// <summary>
/// Store all scenes in here.
/// If we have 3 scenes, we have 3 entries.
/// </summary>
[System.Serializable]
public class GameObjectSave
{
    // string key == scene name
    public Dictionary<string, SceneSave> m_SceneData;

    /// <summary>
    /// Default constructor
    /// </summary>
    public GameObjectSave()
    {
        m_SceneData = new Dictionary<string, SceneSave>();
    }

    /// <summary>
    /// A constructor to pass data
    /// </summary>
    /// <param name="sceneData"></param>
    public GameObjectSave( Dictionary<string, SceneSave> sceneData )
    {
        this.m_SceneData = sceneData;
    }
}