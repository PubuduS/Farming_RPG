/// <summary>
/// Interface for unity ISaveable
/// </summary>
public interface ISaveable
{
    string ISaveableUniqueID { get; set; }

    GameObjectSave GameObjectSave { get; set; }

    void ISaveableRegister();

    void ISaveableDeregister();

    void ISaveableStoreScene( string sceneName );

    void ISaveableRestoreScene( string sceneName );
}