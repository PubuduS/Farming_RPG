[System.Serializable]
/// <summary>
/// This will get the information from serialiable object and populate these class
/// Basically this will store the data in runtime.
/// </summary>
public class GridPropertyDetails
{
    public int m_gridX;
    public int m_gridY;
    public bool m_IsDiggable = false;
    public bool m_CanDropItem = false;
    public bool m_CanPlaceFurniture = false;
    public bool m_IsPath = false;
    public bool m_IsNPCObstacle = false;
    public int m_DaysSinceDug = -1;
    public int m_DaysSinceWatered = -1;
    public int m_SeedItemCode = -1;
    public int m_GrowthDays = -1;
    public int m_DaysSinceLastHarvest = -1;

    /// <summary>
    /// 
    /// </summary>
    public GridPropertyDetails()
    {

    }
}
