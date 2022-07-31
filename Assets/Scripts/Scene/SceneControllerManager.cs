using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manage the transition between scenes
/// </summary>
public class SceneControllerManager : SingletonMonobehaviour<SceneControllerManager>
{
    
    [SerializeField] private float m_FadeDuration = 1.0f;
    [SerializeField] private CanvasGroup m_FaderCanvasGroup = null;
    [SerializeField] private Image m_FaderImage = null;

    private bool m_IsFading;

    public SceneName m_StartingSceneName;

    /// <summary>
    /// Handle the fade in blocking and unblocking inputs.
    /// </summary>
    /// <param name="finalAlpha"></param>
    /// <returns></returns>
    private IEnumerator Fade( float finalAlpha )
    {
        // Set the fading flag to true so the FadeAndSwitchScenes coroutine won't be called again.
        m_IsFading = true;

        // Make sure the CanvasGroup blocks raycast into the scene so no more input can be accepted.
        m_FaderCanvasGroup.blocksRaycasts = true;

        // Calculate how fast CanvasGroup should fade based on it's current alpha, it's final alpha and how long it has to change between two.
        float fadeSpeed = Mathf.Abs( m_FaderCanvasGroup.alpha - finalAlpha) / m_FadeDuration;

        // While the CanvasGroup hasn't reached the final alpha yet...
        while( !Mathf.Approximately( m_FaderCanvasGroup.alpha, finalAlpha) )
        {
            // ...Move the alpha towards it's target alpha.
            m_FaderCanvasGroup.alpha = Mathf.MoveTowards( m_FaderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime );

            // Wait for a frame then continue.
            yield return null;
        }

        // Set the flag to false since the fade has finished
        m_IsFading = false;

        // Stop the CanvasGroup from blocking raycasts so input is no longer ignored.
        m_FaderCanvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Handle fading, switching and unloading scenes.
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    private IEnumerator FadeAndSwitchScenes( string sceneName, Vector3 spawnPosition )
    {
        // Call before scene unload fade out event.
        EventHandler.CallBeforeSceneUnloadFadeOutEvent();

        // Start fading to black and wait for it to finish before continuing.
        yield return StartCoroutine( Fade( 1.0f ) );

        // Store Scene Data
        SaveLoadManager.Instance.StoreCurrentSceneData();

        // Set player position
        Player.Instance.gameObject.transform.position = spawnPosition;

        // Call before scene unload event
        EventHandler.CallBeforeSceneUnloadEvent();

        // Unload the current active scene.
        yield return SceneManager.UnloadSceneAsync( SceneManager.GetActiveScene().buildIndex );

        // Start loading the given scene and wait for it to finish.
        yield return StartCoroutine( LoadSceneAndSetActive( sceneName ) );

        // Call after scene load event.
        EventHandler.CallAfterSceneLoadEvent();

        // Restore new scene data
        SaveLoadManager.Instance.RestoreCurrentSceneData();

        // Start fading back in and wait for it to finish before exiting the function.
        yield return StartCoroutine( Fade(0.0f) );

        // Call after scene load fade in event.
        EventHandler.CallAfterSceneLoadFadeInEvent();
    }

    /// <summary>
    /// Load the scene and the activate it.
    /// Basically, we are additing our new scene to the Persistent scene.
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadSceneAndSetActive( string sceneName )
    {
        // Allow the given scene to load over several frames and add it to the already loaded scenes ( just the Persistent scene at this point).
        yield return SceneManager.LoadSceneAsync( sceneName, LoadSceneMode.Additive );

        // Find the scene that was most recently loaded (the one at the last index of the loaded scenes).
        Scene newlyLoadedScene = SceneManager.GetSceneAt( SceneManager.sceneCount - 1 );

        // Set the newly loaded scene as the active scene (this marks it as the one to be unloaded next).
        SceneManager.SetActiveScene( newlyLoadedScene );
    }

    /// <summary>
    /// Load the starting scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        // Set the initial alpha to start off with a black screen.
        m_FaderImage.color = new Color( 0f, 0f, 0f, 1f );
        m_FaderCanvasGroup.alpha = 1f;

        // Star the first scene loading and wait for it to finish.
        yield return StartCoroutine( LoadSceneAndSetActive( m_StartingSceneName.ToString() ) );

        // If this event has any subscribers, call it.
        EventHandler.CallAfterSceneLoadEvent();

        // Restore new scene data
        SaveLoadManager.Instance.RestoreCurrentSceneData();

        // Once the scene is finished loading, start fading in.
        StartCoroutine( Fade( 0f ) );

    }

    /// <summary>
    /// This is the main externam point of contact and influence from the rest of the project.
    /// This will be called when the player wants to switch scenes.
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="spawnPosition"></param>
    public void FadeAndLoadScene( string sceneName, Vector3 spawnPosition )
    {
        // If a fade isn't happening then start fading and switching scenes.
        if( !m_IsFading )
        {
            StartCoroutine( FadeAndSwitchScenes( sceneName, spawnPosition ) );
        }
    }
}
