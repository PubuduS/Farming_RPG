using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is just a test script to trigger and test animations.
/// </summary>
public class PlayerAnimationTest : MonoBehaviour
{    
    public float m_XInput;
    public float m_YInput;
    public bool m_IsWalking;
    public bool m_IsRunning;
    public bool m_IsIdle;
    public bool m_IsCarrying;
    public ToolEffect m_ToolEffect;
    public bool m_IsUsingToolRight;
    public bool m_IsUsingToolLeft;
    public bool m_IsUsingToolUp;
    public bool m_IsUsingToolDown;
    public bool m_IsLiftingToolRight;
    public bool m_IsLiftingToolLeft;
    public bool m_IsLiftingToolUp;
    public bool m_IsLiftingToolDown;
    public bool m_IsSwingingToolRight;
    public bool m_IsSwingingToolLeft;
    public bool m_IsSwingingToolUp;
    public bool m_IsSwingingToolDown;
    public bool m_IsPickingRight;
    public bool m_IsPickingLeft;
    public bool m_IsPickingUp;
    public bool m_IsPickingDown;   
    public bool m_IdleUp;
    public bool m_IdleDown;
    public bool m_IdleRight;
    public bool m_IdleLeft;

    private void Update()
    {
        EventHandler.CallMovementEvent( m_XInput, m_YInput,
                                        m_IsWalking, m_IsRunning, m_IsIdle, m_IsCarrying,
                                        m_ToolEffect,
                                        m_IsUsingToolRight, m_IsUsingToolLeft, m_IsUsingToolUp, m_IsUsingToolDown,
                                        m_IsLiftingToolRight, m_IsLiftingToolLeft, m_IsLiftingToolUp, m_IsLiftingToolDown,
                                        m_IsPickingRight, m_IsPickingLeft, m_IsPickingUp, m_IsPickingDown,
                                        m_IsSwingingToolRight, m_IsSwingingToolLeft, m_IsSwingingToolUp, m_IsSwingingToolDown,
                                        m_IdleRight, m_IdleLeft, m_IdleUp, m_IdleDown);


    }
}
