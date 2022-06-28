
/// <summary>
/// Describes all the animation states
/// </summary>
public enum AnimationName
{
    IDLE_DOWN,
    IDLE_UP,
    IDLE_RIGHT,
    IDLE_LEFT,

    WALK_UP,
    WALK_DOWN,
    WALK_RIGHT,
    WALK_LEFT,

    RUN_UP,
    RUN_DOWN,    
    RUN_RIGHT,
    RUN_LEFT,

    USE_TOOL_UP,
    USE_TOOL_DOWN,
    USE_TOOL_RIGHT,
    USE_TOOL_LEFT,

    SWING_TOOL_UP,
    SWING_TOOL_DOWN,
    SWING_TOOL_RIGHT,
    SWING_TOOL_LEFT,

    LIFT_TOOL_UP,
    LIFT_TOOL_DOWN,
    LIFT_TOOL_RIGHT,
    LIFT_TOOL_LEFT,

    HOLD_TOOL_UP,
    HOLD_TOOL_DOWN,
    HOLD_TOOL_RIGHT,
    HOLD_TOOL_LEFT,

    PICK_UP,
    PICK_DOWN,
    PICK_RIGHT,
    PICK_LEFT,

    COUNT
}

/// <summary>
/// Describes the character part animator.
/// Each body part has a separate animator
/// </summary>
public enum CharacterPartAnimator
{
    body,
    arms,
    hair,
    tool,
    hat,
    count
}

/// <summary>
/// Describe the part variant color
/// Currently we don't use that because we don't have 
/// the assets with different colors.
/// </summary>
public enum PartVariantColor
{
    NONE,
    COUNT
}

/// <summary>
/// Describes the part variant type
/// </summary>
public enum PartVariantType
{
    NONE,
    CARRY,
    HOE,
    PICKAXE,
    AXE,
    SCYTHE,
    WATERINGCAN,
    COUNT
}

/// <summary>
/// Describes the type of inventory and count
/// Ex: player inventory or chest inventory
/// </summary>
public enum InventoryLocation
{
    Player,
    Chest,
    Count
}

/// <summary>
/// Describes the tool effect
/// </summary>
public enum ToolEffect
{
    NONE,
    WATERING
}

/// <summary>
/// Describes the player facing direction
/// </summary>
public enum Direction
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT    
}

/// <summary>
/// Describe the item type
/// </summary>
public enum ItemType
{
    Seed,
    Commodity,
    WateringTool,
    HoeingTool,
    ChoppingTool,
    BreakingTool,
    ReapingTool,
    CollectingTool,
    ReapableScenary,
    Furniture,
    Count,
    None
}
