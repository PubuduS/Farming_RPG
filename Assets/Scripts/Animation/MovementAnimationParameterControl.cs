using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control and trigger animations based on the right moves.
/// </summary>
public class MovementAnimationParameterControl : MonoBehaviour
{

    private Animator m_Animator;

    /// <summary>
    /// Use for initialisation
    /// </summary>
    public void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// When an object is enabled, we subscribed to animations
    /// </summary>
    private void OnEnable()
    {
        EventHandler.m_MovementEvent += SetAnimationParameters;
    }

    /// <summary>
    /// When an object is disable, we unsubscrib from animations
    /// </summary>
    private void OnDisable()
    {
        EventHandler.m_MovementEvent -= SetAnimationParameters;
    }

    /// <summary>
    /// We take the parameters and trigger appopriate animations.
    /// </summary>
    private void SetAnimationParameters(  float xInput, float yInput,
                                          bool isWalking, bool isRunning, bool isIdle, bool isCarrying,
                                          ToolEffect toolEffect,
                                          bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
                                          bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
                                          bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
                                          bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
                                          bool idleRight, bool idleLeft, bool idleUp, bool idleDown )
    {
        m_Animator.SetFloat( Settings.m_XInput, xInput );
        m_Animator.SetFloat( Settings.m_YInput, yInput );
        m_Animator.SetBool( Settings.m_IsWalking, isWalking );
        m_Animator.SetBool( Settings.m_IsRunning, isRunning );

        m_Animator.SetInteger( Settings.m_ToolEffect, (int)toolEffect );

        #region Tool        
        if ( isUsingToolRight )
            m_Animator.SetTrigger( Settings.m_IsUsingToolRight );

        if( isUsingToolLeft )
            m_Animator.SetTrigger( Settings.m_IsUsingToolLeft );

        if ( isUsingToolUp )
            m_Animator.SetTrigger( Settings.m_IsUsingToolUp );

        if ( isUsingToolDown )
            m_Animator.SetTrigger( Settings.m_IsUsingToolDown );
        #endregion

        #region Lifting
        if ( isLiftingToolRight )
            m_Animator.SetTrigger( Settings.m_IsLiftingToolRight );

        if ( isLiftingToolLeft )
            m_Animator.SetTrigger( Settings.m_IsLiftingToolLeft );

        if ( isLiftingToolUp )
            m_Animator.SetTrigger( Settings.m_IsLiftingToolUp );

        if ( isLiftingToolDown )
            m_Animator.SetTrigger( Settings.m_IsLiftingToolDown );
        #endregion

        #region Swing
        if ( isSwingingToolRight )
            m_Animator.SetTrigger( Settings.m_IsSwingingToolRight );

        if ( isSwingingToolLeft )
            m_Animator.SetTrigger( Settings.m_IsSwingingToolLeft );

        if ( isSwingingToolUp )
            m_Animator.SetTrigger( Settings.m_IsSwingingToolUp );

        if ( isSwingingToolDown )
            m_Animator.SetTrigger( Settings.m_IsSwingingToolDown );
        #endregion

        #region Picking
        if ( isPickingRight )
            m_Animator.SetTrigger( Settings.m_IsPickingRight );

        if ( isPickingLeft )
            m_Animator.SetTrigger( Settings.m_IsPickingLeft );

        if ( isPickingUp )
            m_Animator.SetTrigger( Settings.m_IsPickingUp );

        if ( isPickingDown )
            m_Animator.SetTrigger( Settings.m_IsPickingDown );
        #endregion

        #region Idle
        if ( idleUp )
            m_Animator.SetTrigger( Settings.m_IdleUp );

        if ( idleDown )
            m_Animator.SetTrigger( Settings.m_IdleDown );

        if ( idleLeft )
            m_Animator.SetTrigger( Settings.m_IdleLeft );

        if ( idleRight )
            m_Animator.SetTrigger( Settings.m_IdleRight );
        #endregion


    }

    /// <summary>
    /// Play footstep sound via animation events.
    /// </summary>
    private void AnimationEventPlayFootstepSound()
    {

    }
}
