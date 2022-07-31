using UnityEngine;

/// <summary>
/// Assign a unique GUID for the objects.
/// These ids are used to retrive the gameobjects when restoring scene state.
/// </summary>
[ExecuteAlways]
public class GenerateGUID : MonoBehaviour
{
    [SerializeField]
    private string m_GUID = "";

    public string GUID { get => m_GUID; set => m_GUID = value; }

    /// <summary>
    /// Ensure that the object has a guaranteed unique id
    /// </summary>
    public void Awake()
    {
        // Only populate in the editor
        if( !Application.IsPlaying(gameObject) )
        {
            // Ensure that the object has a guaranteed unique id
            if( m_GUID == "" )
            {
                m_GUID = System.Guid.NewGuid().ToString();
            }
        }
    }
}
