using UnityEngine;

/// <summary>
/// This class trigger fade in and out 
/// depending on the distance of the objects 
/// that needs to be fade out once the player is near.
/// </summary>
public class TriggerObscuringItemFader : MonoBehaviour
{
    //! Get the gameobject we have collided with, and then get all the obscuring  item fader components on it and it children - and then trigger the fade out.
    private void OnTriggerEnter2D( Collider2D collision )
    {
        ObscuringItemFader[] obscuringItemFader = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();

        if(  obscuringItemFader.Length > 0 )
        {
            for( int i = 0; i < obscuringItemFader.Length; i++ )
            {
                obscuringItemFader[i].FadeOut();
            }
        }
    }

    //! Get the gameobject we have collided with, and then get all the obscuring  item fader components on it and it children - and then trigger the fade in.
    private void OnTriggerExit2D( Collider2D collision )
    {
        ObscuringItemFader[] obscuringItemFader = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();

        if( obscuringItemFader.Length > 0 )
        {
            for( int i = 0; i < obscuringItemFader.Length; i++ )
            {
                obscuringItemFader[i].FadeIn();
            }
        }
    }
}
