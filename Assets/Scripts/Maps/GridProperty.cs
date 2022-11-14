[System.Serializable]
/// <summary>
/// Holds information about grid of every scene. 
/// </summary>
public class GridProperty
{

    /// X and Y Axis information
    public GridCoordinate m_GridCoordinate;

    /// For example, is this grid diaggable?
    /// Is this grid an obstacle? etc...
    public GridBoolProperty m_GridBoolProperty;

    public bool m_GridBoolValue = false;

    /// <summary>
    /// Construuctor to pass in values.
    /// </summary>
    /// <param name="gridCoordinate"></param>
    /// <param name="gridBoolProperty"></param>
    /// <param name="gridBoolValue"></param>
    public GridProperty( GridCoordinate gridCoordinate, GridBoolProperty gridBoolProperty, bool gridBoolValue )
    {
        this.m_GridCoordinate = gridCoordinate;
        this.m_GridBoolProperty = gridBoolProperty;
        this.m_GridBoolValue = gridBoolValue;
    }
}
