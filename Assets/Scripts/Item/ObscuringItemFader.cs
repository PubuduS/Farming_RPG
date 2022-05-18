using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
/// <summary>
/// Used to obscure items when player is behind them.
/// So that player can see the avatar clearly.
/// </summary>
public class ObscuringItemFader : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Start to fade out the object
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine( FadeOutRoutine() );
    }

    /// <summary>
    ///  Start to fade in the object
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine( FadeInRoutine() );
    }

    /// <summary>
    /// This coroutine will fade in alpha value at each keyframe.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeInRoutine()
    {
        float currentAlpha = m_SpriteRenderer.color.a;

        float distance = 1f - currentAlpha;

        while( 1f - currentAlpha > 0.01f )
        {
            currentAlpha = currentAlpha + distance / Settings.m_FadeInSeconds * Time.deltaTime;
            m_SpriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }

        m_SpriteRenderer.color = new Color(1f, 1f, 1f, 1f );

    }

    /// <summary>
    /// This coroutine will fade out alpha value at each keyframe.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOutRoutine()
    {
        float currentAlpha = m_SpriteRenderer.color.a;

        float distance = currentAlpha - Settings.m_TargetAlpha;

        while( currentAlpha - Settings.m_TargetAlpha > 0.01f )
        {
            currentAlpha = currentAlpha - distance / Settings.m_FadeOutSeconds * Time.deltaTime;
            m_SpriteRenderer.color = new Color( 1f, 1f, 1f, currentAlpha );
            yield return null;
        }

        m_SpriteRenderer.color = new Color( 1f, 1f, 1f, Settings.m_TargetAlpha );
    }


}
