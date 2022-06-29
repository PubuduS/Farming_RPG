using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton manager class for time managements.
/// </summary>
public class TimeManager : SingletonMonobehaviour<TimeManager>
{
    private int m_GameYear = 1;
    private Season m_GameSeason = Season.Spring;
    private int m_GameDay = 1;
    private int m_GameHour = 6;
    private int m_GameMinute = 30;
    private int m_GameSecond = 0;
    private string m_GameDayOfWeek = "Mon";

    //! Check the game paused or not
    private bool m_IsGameClockPaused = false;

    private float m_GameTick = 0f;

    /// <summary>
    /// Call minute event
    /// </summary>
    private void Start()
    {
        EventHandler.CallAdvanceGameMinuteEvent( m_GameYear, m_GameSeason, m_GameDay, m_GameDayOfWeek, m_GameHour, m_GameMinute, m_GameSecond );
    }

    /// <summary>
    /// Start the ticking
    /// </summary>
    private void Update()
    {
        if( !m_IsGameClockPaused )
        {
            GameTick();
        }
    }

    /// <summary>
    /// Convert delta time to ingame seconds.
    /// </summary>
    private void GameTick()
    {
        m_GameTick += Time.deltaTime;

        if( m_GameTick >= Settings.m_SecondsPerGameSeconds )
        {
            m_GameTick -= Settings.m_SecondsPerGameSeconds;
            UpdateGameSecond();
        }
    }

    /// <summary>
    /// Handles the progression of time.
    /// </summary>
    private void UpdateGameSecond()
    {
        m_GameSecond++;

        if( m_GameSecond > 59 )
        {
            m_GameSecond = 0;
            m_GameMinute++;

            if( m_GameMinute > 59 )
            {
                m_GameMinute = 0;
                m_GameHour++;

                if( m_GameHour > 23 )
                {
                    m_GameHour = 0;
                    m_GameDay++;

                    if( m_GameDay > 30 )
                    {
                        m_GameDay = 1;

                        int gs = (int)m_GameSeason;
                        gs++;

                        m_GameSeason = (Season)gs;

                        if( gs > 3 )
                        {
                            gs = 0;
                            m_GameSeason = (Season)gs;

                            m_GameYear++;

                            if( m_GameYear > 9999 )
                            {
                                m_GameYear = 1;
                            }

                            EventHandler.CallAdvanceGameYearEvent( m_GameYear, m_GameSeason, m_GameDay, m_GameDayOfWeek, m_GameHour, m_GameMinute, m_GameSecond );
                        }

                        EventHandler.CallAdvanceGameSeasonEvent( m_GameYear, m_GameSeason, m_GameDay, m_GameDayOfWeek, m_GameHour, m_GameMinute, m_GameSecond );
                    }

                    m_GameDayOfWeek = GetDayOfWeek();
                    EventHandler.CallAdvanceGameDayEvent( m_GameYear, m_GameSeason, m_GameDay, m_GameDayOfWeek, m_GameHour, m_GameMinute, m_GameSecond );
                }

                EventHandler.CallAdvanceGameHourEvent( m_GameYear, m_GameSeason, m_GameDay, m_GameDayOfWeek, m_GameHour, m_GameMinute, m_GameSecond );
            }

            EventHandler.CallAdvanceGameMinuteEvent( m_GameYear, m_GameSeason, m_GameDay, m_GameDayOfWeek, m_GameHour, m_GameMinute, m_GameSecond );            
        }

        // Call to advance game second event would go here if required.
    }

    /// <summary>
    /// Return the day of the week
    /// </summary>
    /// <returns> (String) dayOfWeek </returns>
    private string GetDayOfWeek()
    {
        int totalDays = ( ( (int)m_GameSeason ) * 30 ) + m_GameDay;
        int dayOfWeek = totalDays % 7;

        switch( dayOfWeek )
        {
            case 1:
                return "Mon";

            case 2:
                return "Tue";

            case 3:
                return "Wed";

            case 4:
                return "Thu";

            case 5:
                return "Fri";

            case 6:
                return "Sat";

            case 7:
                return "Sun";

            default:
                return "Err";
        }
    }

    /// TODO: Remove before final product
    /// <summary>
    /// Advance 1 game minute
    /// </summary>
    public void TestAdvanceGameMinute()
    {
        for( int i = 0; i < 60; i++ )
        {
            UpdateGameSecond();
        }
    }

    /// TODO: Remove before final product
    /// <summary>
    /// Advance 1 game day
    /// </summary>
    public void TestAdvanceGameDay()
    {
        for( int i = 0; i < 86400; i++ )
        {
            UpdateGameSecond();
        }
    }
}
