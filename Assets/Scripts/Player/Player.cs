using UnityEngine;

/// <summary>
/// This class has all the things related to player
/// </summary>
public class Player : SingletonMonobehaviour<Player>
{
    // Movement Parameters
    private float m_XInput;
    private float m_YInput;
    private bool m_IsWalking;
    private bool m_IsRunning;
    private bool m_IsIdle;
    private bool m_IsCarrying = false;    
    private bool m_IsUsingToolRight;
    private bool m_IsUsingToolLeft;
    private bool m_IsUsingToolUp;
    private bool m_IsUsingToolDown;
    private bool m_IsLiftingToolRight;
    private bool m_IsLiftingToolLeft;
    private bool m_IsLiftingToolUp;
    private bool m_IsLiftingToolDown;
    private bool m_IsSwingingToolRight;
    private bool m_IsSwingingToolLeft;
    private bool m_IsSwingingToolUp;
    private bool m_IsSwingingToolDown;
    private bool m_IsPickingRight;
    private bool m_IsPickingLeft;
    private bool m_IsPickingUp;
    private bool m_IsPickingDown;
    private ToolEffect m_ToolEffect = ToolEffect.NONE;

    private Camera m_MainCamera;

    private Rigidbody2D m_RigidBody2D;
    private Direction m_PlayerDirection;
    private float m_MovementSpeed;

    private bool m_PlayerInputIsDisabled = false;

    public bool PlayerInputIsDisabled
    {
        get => m_PlayerInputIsDisabled;
        set => m_PlayerInputIsDisabled = value;
    }

    /// <summary>
    /// Initialize members
    /// </summary>
    private void Awake()
    {
        base.Awake();
        m_RigidBody2D = GetComponent<Rigidbody2D>();

        // Get a reference to the main camera
        m_MainCamera = Camera.main;
    }

    /// <summary>
    /// Handle movement inputs
    /// and publish animation events.
    /// </summary>
    private void Update()
    {
        #region Player Input

        ResetAnimatioTriggers();

        PlayerMovementInput();

        PlayerWalkInput();

        // Send event to any listeners for player movement input
        EventHandler.CallMovementEvent( m_XInput, m_YInput,
                                m_IsWalking, m_IsRunning, m_IsIdle, m_IsCarrying,
                                m_ToolEffect,
                                m_IsUsingToolRight, m_IsUsingToolLeft, m_IsUsingToolUp, m_IsUsingToolDown,
                                m_IsLiftingToolRight, m_IsLiftingToolLeft, m_IsLiftingToolUp, m_IsLiftingToolDown,
                                m_IsPickingRight, m_IsPickingLeft, m_IsPickingUp, m_IsPickingDown,
                                m_IsSwingingToolRight, m_IsSwingingToolLeft, m_IsSwingingToolUp, m_IsSwingingToolDown,
                                false, false, false, false );

        #endregion
    }

    /// <summary>
    /// Handle player movements.
    /// </summary>
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    /// <summary>
    /// Calculate player movements.
    /// </summary>
    private void PlayerMovement()
    {
        Vector2 move = new Vector2( m_XInput * m_MovementSpeed * Time.deltaTime, m_YInput * m_MovementSpeed * Time.deltaTime );
        m_RigidBody2D.MovePosition( m_RigidBody2D.position + move );
    }

    /// <summary>
    /// Reset the animation triggers.
    /// </summary>
    private void ResetAnimatioTriggers()
    {        
        m_IsUsingToolRight = false;
        m_IsUsingToolLeft = false;
        m_IsUsingToolUp = false;
        m_IsUsingToolDown = false;
        m_IsLiftingToolRight = false;
        m_IsLiftingToolLeft = false;
        m_IsLiftingToolUp = false;
        m_IsLiftingToolDown = false;
        m_IsSwingingToolRight = false;
        m_IsSwingingToolLeft = false;
        m_IsSwingingToolUp = false;
        m_IsSwingingToolDown = false;
        m_IsPickingRight = false;
        m_IsPickingLeft = false;
        m_IsPickingUp = false;
        m_IsPickingDown = false;
        m_ToolEffect = ToolEffect.NONE;
    }

    /// <summary>
    /// Handle player inputs and normalized that.
    /// </summary>
    private void PlayerMovementInput()
    {
        m_XInput = Input.GetAxisRaw( "Horizontal" );
        m_YInput = Input.GetAxisRaw( "Vertical" );

        // Handle where you press both x and y at the same time.
        // Move Diagonally with the same speed.
        if( ( m_XInput != 0 ) || ( m_YInput != 0 ) )
        {
            m_XInput = m_XInput * 0.71f;
            m_YInput = m_YInput * 0.71f;
        }

        if( ( m_XInput != 0 ) || ( m_YInput != 0 ) )
        {
            m_IsRunning = true;
            m_IsWalking = false;
            m_IsIdle = false;
            m_MovementSpeed = Settings.m_RunningSpeed;

            // Capture player direction for save game
            if( m_XInput < 0 )
            {
                m_PlayerDirection = Direction.LEFT;
            }
            else if( m_XInput > 0 )
            {
                m_PlayerDirection = Direction.RIGHT;
            }
            else if( m_YInput < 0 )
            {
                m_PlayerDirection = Direction.DOWN;
            }
            else
            {
                m_PlayerDirection = Direction.UP;
            }
        }
        else if( ( m_XInput == 0 ) && ( m_YInput == 0 ) )
        {
            m_IsRunning = false;
            m_IsWalking = false;
            m_IsIdle = true;
        }
    }

    /// <summary>
    /// Handle player walk inputs.
    /// </summary>
    private void PlayerWalkInput()
    {
        if( Input.GetKey( KeyCode.LeftShift ) || Input.GetKey( KeyCode.RightShift ) )
        {
            m_IsRunning = false;
            m_IsWalking = true;
            m_IsIdle = false;
            m_MovementSpeed = Settings.m_WalkingSpeed;
        }
        else
        {
            m_IsRunning = true;
            m_IsWalking = false;
            m_IsIdle = false;
            m_MovementSpeed = Settings.m_RunningSpeed;
        }
    }

    public Vector3 GetPlayerViewportPosition()
    {
        // Vector3 viewport position for player (0, 0) viewport bottom left, (1, 1) viewport top right
        return m_MainCamera.WorldToViewportPoint( transform.position );
    }
}
