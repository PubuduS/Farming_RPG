using System.Collections;
using UnityEngine;

/// <summary>
/// Create a class that create nudge effect on grass and some other items.
/// When player get near items, it will create a wobbly effect.
/// </summary>
public class ItemNudge : MonoBehaviour
{
    private WaitForSeconds m_Pause;

    private bool m_IsAnimating = false;

    private void Awake()
    {
        m_Pause = new WaitForSeconds( 0.04f );
    }

    /// <summary>
    /// Detect and starts coroutines when player collided with an object that this script is attached to
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( m_IsAnimating == false )
        {
            if(  gameObject.transform.position.x < collision.gameObject.transform.position.x )
            {
                StartCoroutine( RotateAntiClock() );
            }
            else
            {
                StartCoroutine( RotateClock() );
            }
        }
    }

    /// <summary>
    /// Detect and starts coroutines when player exit from the collider that this bject is attached to
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( m_IsAnimating == false )
        {
            if( gameObject.transform.position.x > collision.gameObject.transform.position.x )
            {
                StartCoroutine( RotateAntiClock() );
            }
            else
            {
                StartCoroutine( RotateClock() );
            }
        }
    }

    /// <summary>
    /// Rotate the gameObject anti clock wise.
    /// We rotate it by 2 in 4 steps
    /// Then we rotate it in the opposite direction by 2 in 5 steps
    /// Finally, we return to the original position.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateAntiClock()
    {
        m_IsAnimating = true;

        for( int i = 0; i < 4; i++  )  
        {
            gameObject.transform.GetChild(0).Rotate( 0f, 0f, 2f );
            yield return m_Pause;
        }

        for( int i = 0; i < 5; i++ )
        {
            gameObject.transform.GetChild(0).Rotate( 0f, 0f, -2f );
            yield return m_Pause;
        }

        gameObject.transform.GetChild(0).Rotate( 0f, 0f, 2f );
        yield return m_Pause;

        m_IsAnimating = false;

    }

    /// <summary>
    /// Rotate the gameObject clock wise.
    /// We rotate it by 2 in 4 steps
    /// Then we rotate it in the opposite direction by 2 in 5 steps
    /// Finally, we return to the original position.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateClock()
    {
        m_IsAnimating = true;

        for( int i = 0; i < 4; i++ )
        {
            gameObject.transform.GetChild(0).Rotate( 0f, 0f, -2f );
            yield return m_Pause;
        }

        for( int i = 0; i < 5; i++ )
        {
            gameObject.transform.GetChild(0).Rotate( 0f, 0f, 2f );
            yield return m_Pause;
        }

        gameObject.transform.GetChild(0).Rotate( 0f, 0f, -2f );
        yield return m_Pause;

        m_IsAnimating = false;
    }
}
