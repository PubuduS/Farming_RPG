using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        EventHandler.MovementEvent += SetAnimationParameters;
    }

    /// <summary>
    /// When an object is disable, we unsubscrib from animations
    /// </summary>
    private void OnDisable()
    {
        EventHandler.MovementEvent -= SetAnimationParameters;
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
        m_Animator.SetFloat( Settings.xInput, xInput );
        m_Animator.SetFloat( Settings.yInput, yInput );
        m_Animator.SetBool( Settings.isWalking, isWalking );
        m_Animator.SetBool( Settings.isRunning, isRunning );

        m_Animator.SetInteger( Settings.toolEffect, (int)toolEffect );

        #region Tool        
        if ( isUsingToolRight )
            m_Animator.SetTrigger( Settings.isUsingToolRight );

        if( isUsingToolLeft )
            m_Animator.SetTrigger( Settings.isUsingToolLeft );

        if ( isUsingToolUp )
            m_Animator.SetTrigger( Settings.isUsingToolUp );

        if ( isUsingToolDown )
            m_Animator.SetTrigger( Settings.isUsingToolDown );
        #endregion

        #region Lifting
        if ( isLiftingToolRight )
            m_Animator.SetTrigger( Settings.isLiftingToolRight );

        if ( isLiftingToolLeft )
            m_Animator.SetTrigger( Settings.isLiftingToolLeft );

        if ( isLiftingToolUp )
            m_Animator.SetTrigger( Settings.isLiftingToolUp );

        if ( isLiftingToolDown )
            m_Animator.SetTrigger( Settings.isLiftingToolDown );
        #endregion

        #region Swing
        if ( isSwingingToolRight )
            m_Animator.SetTrigger( Settings.isSwingingToolRight );

        if ( isSwingingToolLeft )
            m_Animator.SetTrigger( Settings.isSwingingToolLeft );

        if ( isSwingingToolUp )
            m_Animator.SetTrigger( Settings.isSwingingToolUp );

        if ( isSwingingToolDown )
            m_Animator.SetTrigger( Settings.isSwingingToolDown );
        #endregion

        #region Picking
        if ( isPickingRight )
            m_Animator.SetTrigger( Settings.isPickingRight );

        if ( isPickingLeft )
            m_Animator.SetTrigger( Settings.isPickingLeft );

        if ( isPickingUp )
            m_Animator.SetTrigger( Settings.isPickingUp );

        if ( isPickingDown )
            m_Animator.SetTrigger( Settings.isPickingDown );
        #endregion

        #region Idle
        if ( idleUp )
            m_Animator.SetTrigger( Settings.idleUp );

        if ( idleDown )
            m_Animator.SetTrigger( Settings.idleDown );

        if ( idleLeft )
            m_Animator.SetTrigger( Settings.idleLeft );

        if ( idleRight )
            m_Animator.SetTrigger( Settings.idleRight );
        #endregion


    }

    /// <summary>
    /// Play footstep sound via animation events.
    /// </summary>
    private void AnimationEventPlayFootstepSound()
    {

    }
}
