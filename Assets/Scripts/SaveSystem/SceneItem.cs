/// <summary>
/// We have scene item instances for every
/// item in the scene that we want to store in a save file.
/// </summary>
[System.Serializable]
public class SceneItem
{
    public int m_ItemCode;
    public Vector3Serializable m_Position;
    public string m_ItemName;

    public SceneItem()
    {
        m_Position = new Vector3Serializable();
    }
}