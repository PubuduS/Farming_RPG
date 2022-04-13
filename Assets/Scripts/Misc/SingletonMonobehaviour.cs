using UnityEngine;

/// <summary>
/// Abstract class that inherited by all the game managers.
/// All the game managers use singleton since game manager always have 1 instance.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonMonobehaviour<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T g_Instance;

    public static T Instance 
    {
        get
        {
            return g_Instance;
        }
    }

    protected virtual void Awake()
    {
        if( g_Instance == null )
        {
            g_Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
