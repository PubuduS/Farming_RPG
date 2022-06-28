using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_AnimationType", menuName = "Scriptable Objects/Animation/Animation Type")]

/// Create a scriptable animation type.
/// This objects holds infomration about animation variants.
public class SO_AnimationType : ScriptableObject
{
    public AnimationClip m_AnimationClip;
    public AnimationName m_AnimationName;
    public CharacterPartAnimator m_CharacterPart;
    public PartVariantColor m_PartVariantColor;
    public PartVariantType m_PartVariantType;
}
