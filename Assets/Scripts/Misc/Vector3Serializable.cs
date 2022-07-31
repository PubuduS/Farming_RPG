
/// <summary>
/// Unity Vector3 data type is NOT serializable.
/// Therefore, it can't be searilize and saved to a datafile.
/// This class is designed to achieve that.
/// </summary>
[System.Serializable]
public class Vector3Serializable
{
    public float m_X;
    public float m_Y;
    public float m_Z;

    /// <summary>
    /// Constructor to pass in position data
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public Vector3Serializable( float x, float y, float z )
    {
        this.m_X = x;
        this.m_Y = y;
        this.m_Z = z;
    }

    public Vector3Serializable()
    {
    }
}
