using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneTeleport : MonoBehaviour
{

    [SerializeField] private SceneName m_SceneNameGoTo = SceneName.Scene1_Farm;
    [SerializeField] private Vector3 m_ScenePositionGoTo = new Vector3();

    /// <summary>
    /// If the collision happened with player, then teleport it to next scene.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D( Collider2D collision )
    {
        Player player = collision.GetComponent<Player>();

        if( player != null )
        {
            // Calculate the player's new position
            float xPosition = Mathf.Approximately( m_ScenePositionGoTo.x, 0f ) ? player.transform.position.x : m_ScenePositionGoTo.x;

            float yPosition = Mathf.Approximately(m_ScenePositionGoTo.y, 0f) ? player.transform.position.y : m_ScenePositionGoTo.y;

            float zPosition = 0f;

            // Teleport to new scene.
            SceneControllerManager.Instance.FadeAndLoadScene( m_SceneNameGoTo.ToString(), new Vector3( xPosition, yPosition, zPosition ) );

        }
    }
}
