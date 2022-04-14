public delegate void MovementDelegate( float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle, bool isCarrying,
    ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool idleRight, bool idleLeft, bool idleUp, bool idleDown );

public static class EventHandler
{
    // Movement Event
    public static event MovementDelegate m_MovementEvent;

    // Movement Event Call For Publishers
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
}