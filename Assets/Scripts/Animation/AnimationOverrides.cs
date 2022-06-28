using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOverrides : MonoBehaviour
{
    [SerializeField] private GameObject m_Character = null;
    [SerializeField] private SO_AnimationType[] m_SOAnimationTypeArray = null;

    private Dictionary<AnimationClip, SO_AnimationType> m_AnimationTypeDictionaryByAnimation;
    private Dictionary<string, SO_AnimationType> m_AnimationTypeDictionaryByCompositeAttributeKey;

    /// <summary>
    /// Initialize the lists
    /// </summary>
    private void Start()
    {
        // Initialize animation type dictionary keyed by animation key
        m_AnimationTypeDictionaryByAnimation = new Dictionary<AnimationClip, SO_AnimationType>();

        foreach( SO_AnimationType item in m_SOAnimationTypeArray )
        {
            m_AnimationTypeDictionaryByAnimation.Add( item.m_AnimationClip, item );
        }

        // Initialize animation type dictionary keyed by string
        m_AnimationTypeDictionaryByCompositeAttributeKey = new Dictionary<string, SO_AnimationType>();

        foreach( SO_AnimationType item in m_SOAnimationTypeArray )
        {
            string key = item.m_CharacterPart.ToString() + item.m_PartVariantColor.ToString() + item.m_PartVariantType.ToString() + item.m_AnimationName.ToString();
            m_AnimationTypeDictionaryByCompositeAttributeKey.Add( key, item );
        }

    }

    /// <summary>
    /// Handles the animation overrides
    /// </summary>
    /// <param name="characterAttributesList"></param>
    public void ApplyCharacterCustomisationParameters( List<CharacterAttribute> characterAttributesList )
    {
        // Loop through all character attributes and set the animation override controller for each
        foreach( CharacterAttribute characterAttribute in characterAttributesList )
        {
            Animator currentAnimator = null;
            List<KeyValuePair<AnimationClip, AnimationClip>> animsKeyValuePairList = new List<KeyValuePair<AnimationClip, AnimationClip>>();

            string animatorSOAssetName = characterAttribute.characterPart.ToString();

            // Find animators in scene that match scriptable object animator type
            Animator[] animatorsArray = m_Character.GetComponentsInChildren<Animator>();

            foreach( Animator animator in animatorsArray )
            {
                if ( animator.name == animatorSOAssetName )
                {
                    currentAnimator = animator;
                    break;
                }
            }

            // Get base current animations for animator
            AnimatorOverrideController aoc = new AnimatorOverrideController( currentAnimator.runtimeAnimatorController );
            List<AnimationClip> animationList = new List<AnimationClip>( aoc.animationClips );

            foreach( AnimationClip animationClip in animationList )
            {
                // Find animation in dictionary
                SO_AnimationType so_AnimationType;

                bool hasFoundAnimation = m_AnimationTypeDictionaryByAnimation.TryGetValue( animationClip, out so_AnimationType );

                if( hasFoundAnimation )
                {
                    string key = characterAttribute.characterPart.ToString() + characterAttribute.partVariantColor.ToString() + characterAttribute.partVariantType.ToString() + so_AnimationType.m_AnimationName.ToString();

                    SO_AnimationType swapSO_AnimationType;

                    bool hasFoundSwapAnimation = m_AnimationTypeDictionaryByCompositeAttributeKey.TryGetValue( key, out swapSO_AnimationType );

                    if( hasFoundAnimation )
                    {
                        AnimationClip swapAnimationClip = swapSO_AnimationType.m_AnimationClip;

                        animsKeyValuePairList.Add( new KeyValuePair<AnimationClip, AnimationClip>( animationClip, swapAnimationClip ) );
                    }
                }
            }

            // Apply animation updates to animation override controller and then update animator with the new controller
            aoc.ApplyOverrides( animsKeyValuePairList );
            currentAnimator.runtimeAnimatorController = aoc;
        }
    }
}
