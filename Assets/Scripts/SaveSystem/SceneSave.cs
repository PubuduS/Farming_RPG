using System.Collections.Generic;

/// <summary>
/// Store all of the scene items in this list.
/// </summary>
[System.Serializable]
public class SceneSave
{
    // string key is an identifier name we choose for this list
    public Dictionary<string, List<SceneItem>> m_ListSceneItemDictionary;
}