using UnityEngine;

[System.Serializable]
/// <summary>
/// Holds grid information x and y coordinate.
/// </summary>
public class GridCoordinate
{
    /// Define x Axis    
    public int x;

    /// Define y Axis    
    public int y;

    /// <summary>
    /// Constructor to pass x and y axis data
    /// </summary>
    /// <param name="xAxis"></param>
    /// <param name="yAxis"></param>
    public GridCoordinate( int xAxis, int yAxis )
    {
        x = xAxis;
        y = yAxis;
    }

    /// <summary>
    /// Explicitly convert grid coordinate to Vector2 values.
    /// </summary>
    /// <param name="gridCoordinate"></param>
    public static explicit operator Vector2( GridCoordinate gridCoordinate )
    {
        return new Vector2( (float)gridCoordinate.x, (float)gridCoordinate.y );
    }

    /// <summary>
    /// Explicitly convert grid coordinate to Vector2Int values.
    /// </summary>
    /// <param name="gridCoordinate"></param>
    public static explicit operator Vector2Int( GridCoordinate gridCoordinate )
    {
        return new Vector2Int( gridCoordinate.x, gridCoordinate.y );
    }

    /// <summary>
    /// Explicitly convert grid coordinate to Vector3 values.
    /// </summary>
    /// <param name="gridCoordinate"></param>
    public static explicit operator Vector3( GridCoordinate gridCoordinate )
    {
        return new Vector3( (float)gridCoordinate.x, (float)gridCoordinate.y, 0f );
    }

    /// <summary>
    /// Explicitly convert grid coordinate to Vector3Int values.
    /// </summary>
    /// <param name="gridCoordinate"></param>
    public static explicit operator Vector3Int(GridCoordinate gridCoordinate)
    {
        return new Vector3Int( gridCoordinate.x, gridCoordinate.y, 0 );
    }
}
