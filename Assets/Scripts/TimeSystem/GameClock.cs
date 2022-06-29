using TMPro;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TimeText = null;
    [SerializeField] private TextMeshProUGUI m_DateText = null;
    [SerializeField] private TextMeshProUGUI m_SeasonText = null;
    [SerializeField] private TextMeshProUGUI m_YearText = null;

    /// <summary>
    /// Subscribe to the AdvanceGameMinuteEvent
    /// </summary>
    private void OnEnable()
    {
        EventHandler.AdvanceGameMinuteEvent += UpdateGameTime;
    }

    /// <summary>
    /// Unsubscribe from the AdvanceGameMinuteEvent
    /// </summary>
    private void OnDisable()
    {
        EventHandler.AdvanceGameMinuteEvent -= UpdateGameTime;
    }

    /// <summary>
    /// Update UI game time
    /// </summary>
    /// <param name="gameYear"></param>
    /// <param name="gameSeason"></param>
    /// <param name="gameDay"></param>
    /// <param name="gameDayOfWeek"></param>
    /// <param name="gameHour"></param>
    /// <param name="gameMinute"></param>
    /// <param name="gameSecond"></param>
    private void UpdateGameTime( int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond )
    {
        // Update Time

        gameMinute = gameMinute - ( gameMinute % 10 );

        string ampm = "";
        string minute;

        if( gameHour >= 12 )
        {
            ampm = " pm";
        }
        else
        {
            ampm = " am";
        }

        if( gameHour >= 13 )
        {
            gameHour -= 12;
        }

        if( gameMinute < 10 )
        {
            minute = "0" + gameMinute.ToString();
        }
        else
        {
            minute = gameMinute.ToString();
        }

        string time = gameHour.ToString() + " : " + minute + ampm;

        m_TimeText.SetText( time );
        m_DateText.SetText( gameDayOfWeek + ". " + gameDay.ToString());
        m_SeasonText.SetText( gameSeason.ToString() );
        m_YearText.SetText( "Year " + gameYear );
    }
}
