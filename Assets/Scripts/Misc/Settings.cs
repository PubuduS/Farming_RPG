﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    // Player Movements
    public const float m_RunningSpeed = 5.333f;
    public const float m_WalkingSpeed = 2.666f;

    // Player Animation Parameters
    public static int m_XInput;
    public static int m_YInput;
    public static int m_IsWalking;
    public static int m_IsRunning;
    public static int m_ToolEffect;
    public static int m_IsUsingToolRight;
    public static int m_IsUsingToolLeft;
    public static int m_IsUsingToolUp;
    public static int m_IsUsingToolDown;
    public static int m_IsLiftingToolRight;
    public static int m_IsLiftingToolLeft;
    public static int m_IsLiftingToolUp;
    public static int m_IsLiftingToolDown;
    public static int m_IsSwingingToolRight;
    public static int m_IsSwingingToolLeft;
    public static int m_IsSwingingToolUp;
    public static int m_IsSwingingToolDown;
    public static int m_IsPickingRight;
    public static int m_IsPickingLeft;
    public static int m_IsPickingUp;
    public static int m_IsPickingDown;

    // Shared Animation Parameters
    public static int m_IdleUp;
    public static int m_IdleDown;
    public static int m_IdleRight;
    public static int m_IdleLeft;

    // static constructor
    static Settings()
    {
        // Player Animation Parameters
        m_XInput = Animator.StringToHash("xInput");
        m_YInput = Animator.StringToHash("yInput");
        m_IsWalking = Animator.StringToHash("isWalking");
        m_IsRunning = Animator.StringToHash("isRunning");
        m_ToolEffect = Animator.StringToHash("toolEffect");
        m_IsUsingToolRight = Animator.StringToHash("isUsingToolRight");
        m_IsUsingToolLeft = Animator.StringToHash("isUsingToolLeft");
        m_IsUsingToolUp = Animator.StringToHash("isUsingToolUp");
        m_IsUsingToolDown = Animator.StringToHash("isUsingToolDown");
        m_IsLiftingToolRight = Animator.StringToHash("isLiftingToolRight");
        m_IsLiftingToolLeft = Animator.StringToHash("isLiftingToolLeft");
        m_IsLiftingToolUp = Animator.StringToHash("isLiftingToolUp");
        m_IsLiftingToolDown = Animator.StringToHash("isLiftingToolDown");
        m_IsSwingingToolRight = Animator.StringToHash("isSwingingToolRight");
        m_IsSwingingToolLeft = Animator.StringToHash("isSwingingToolLeft");
        m_IsSwingingToolUp = Animator.StringToHash("isSwingingToolUp");
        m_IsSwingingToolDown = Animator.StringToHash("isSwingingToolDown");
        m_IsPickingRight = Animator.StringToHash("isPickingRight");
        m_IsPickingLeft = Animator.StringToHash("isPickingLeft");
        m_IsPickingUp = Animator.StringToHash("isPickingUp");
        m_IsPickingDown = Animator.StringToHash("isPickingDown");

        // Shared Animation Parameters
        m_IdleUp = Animator.StringToHash("idleUp");
        m_IdleDown = Animator.StringToHash("idleDown");
        m_IdleLeft = Animator.StringToHash("idleLeft");
        m_IdleRight = Animator.StringToHash("idleRight");
    }

}
