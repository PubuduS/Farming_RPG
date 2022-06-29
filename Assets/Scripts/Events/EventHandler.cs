using System;
using System.Collections.Generic;

public delegate void MovementDelegate( float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle, bool isCarrying,
    ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool idleRight, bool idleLeft, bool idleUp, bool idleDown );

/// <summary>
/// Raise an handle event when a movement is triggered.
/// </summary>
public static class EventHandler
{

    //! Inventory Updated Event
    public static event Action<InventoryLocation, List<InventoryItem>> m_InventoryUpdatedEvent;

    /// <summary>
    /// Update the event
    /// </summary>
    /// <param name="inventoryLocation"></param>
    /// <param name="inventoryList"></param>
    public static void CallInventoryUpdatedEvent( InventoryLocation inventoryLocation, List<InventoryItem> inventoryList )
    {
        if( m_InventoryUpdatedEvent != null )
        {
            m_InventoryUpdatedEvent( inventoryLocation, inventoryList );
        }
    }

    //! Movement Event
    public static event MovementDelegate m_MovementEvent;

    //! Movement Event Call For Publishers
    public static void CallMovementEvent( float xInput, float yInput, 
                                          bool isWalking, bool isRunning, bool isIdle, bool isCarrying,
                                          ToolEffect toolEffect, 
                                          bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
                                          bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
                                          bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
                                          bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
                                          bool idleRight, bool idleLeft, bool idleUp, bool idleDown )
    {
        if( m_MovementEvent != null )
        {
            m_MovementEvent( xInput, yInput, isWalking, isRunning, isIdle, isCarrying,
                           toolEffect, isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
                           isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
                           isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
                           isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
                           idleRight, idleLeft, idleUp, idleDown );
        }
    }

    //! Time events

    /// <summary>
    /// Advance game minutes
    /// </summary>
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameMinuteEvent;

    /// <summary>
    /// Call the event AdvanceGameMinuteEvent
    /// </summary>
    /// <param name="gameYear"></param>
    /// <param name="gameSeason"></param>
    /// <param name="gameDay"></param>
    /// <param name="gameDayOfWeek"></param>
    /// <param name="gameHour"></param>
    /// <param name="gameMinute"></param>
    /// <param name="gameSecond"></param>
    public static void CallAdvanceGameMinuteEvent( int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond )
    {
        if( AdvanceGameMinuteEvent != null )
        {
            AdvanceGameMinuteEvent( gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond );
        }
    }

    /// <summary>
    /// Advance game hours
    /// </summary>
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameHourEvent;

    /// <summary>
    /// Call the event AdvanceGameHourEvent
    /// </summary>
    /// <param name="gameYear"></param>
    /// <param name="gameSeason"></param>
    /// <param name="gameDay"></param>
    /// <param name="gameDayOfWeek"></param>
    /// <param name="gameHour"></param>
    /// <param name="gameMinute"></param>
    /// <param name="gameSecond"></param>
    public static void CallAdvanceGameHourEvent( int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond )
    {
        if( AdvanceGameHourEvent != null )
        {
            AdvanceGameHourEvent( gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond );
        }
    }

    /// <summary>
    /// Advance game days
    /// </summary>
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameDayEvent;

    /// <summary>
    /// Call the event AdvanceGameDayEvent
    /// </summary>
    /// <param name="gameYear"></param>
    /// <param name="gameSeason"></param>
    /// <param name="gameDay"></param>
    /// <param name="gameDayOfWeek"></param>
    /// <param name="gameHour"></param>
    /// <param name="gameMinute"></param>
    /// <param name="gameSecond"></param>
    public static void CallAdvanceGameDayEvent( int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond )
    {
        if( AdvanceGameDayEvent != null )
        {
            AdvanceGameDayEvent( gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond );
        }
    }

    /// <summary>
    /// Advance game season
    /// </summary>
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameSeasonEvent;

    /// <summary>
    /// Call the event AdvanceGameSeasonEvent
    /// </summary>
    /// <param name="gameYear"></param>
    /// <param name="gameSeason"></param>
    /// <param name="gameDay"></param>
    /// <param name="gameDayOfWeek"></param>
    /// <param name="gameHour"></param>
    /// <param name="gameMinute"></param>
    /// <param name="gameSecond"></param>
    public static void CallAdvanceGameSeasonEvent( int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond )
    {
        if( AdvanceGameSeasonEvent != null )
        {
            AdvanceGameSeasonEvent( gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond );
        }
    }

    /// <summary>
    /// Advance game year
    /// </summary>
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameYearEvent;

    /// <summary>
    /// Call the event AdvanceGameYearEvent
    /// </summary>
    /// <param name="gameYear"></param>
    /// <param name="gameSeason"></param>
    /// <param name="gameDay"></param>
    /// <param name="gameDayOfWeek"></param>
    /// <param name="gameHour"></param>
    /// <param name="gameMinute"></param>
    /// <param name="gameSecond"></param>
    public static void CallAdvanceGameYearEvent( int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond )
    {
        if( AdvanceGameYearEvent != null )
        {
            AdvanceGameYearEvent( gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond );
        }
    }
}