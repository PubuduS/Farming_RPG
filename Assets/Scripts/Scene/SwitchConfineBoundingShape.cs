using UnityEngine;
using Cinemachine;

/// <summary>
/// This class will confine the camera so
/// that it won't go beyond the boundaries.
/// </summary>
public class SwitchConfineBoundingShape : MonoBehaviour
{
    
    /// <summary>
    /// Subscribe to the AfterSceneLoadEvent event.
    /// </summary>
    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += SwitchBoundingShape;
    }

    /// <summary>
    /// Unsubscribe from the AfterSceneLoadEvent event.
    /// </summary>
    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= SwitchBoundingShape;
    }

    /// <summary>
    /// Switch the collider that cinemachine uses to define the edges of the screen.
    /// </summary>
    private void SwitchBoundingShape()
    {
        // Get the polygon colider on the 'boundsconfiner' gameobject which is used by Cinemachine to prevent the camera going beyond the screen edges.
        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BoundsConfiner).GetComponent<PolygonCollider2D>();

        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();

        cinemachineConfiner.m_BoundingShape2D = polygonCollider2D;

        // Since the confiner bounds have changed need to call this to clear the cache.
        cinemachineConfiner.InvalidatePathCache(); 
    }

}
