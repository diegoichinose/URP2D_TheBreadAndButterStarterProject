using UnityEngine;

public class OnApplicationQuitSaveGame : MonoBehaviour
{
    void OnApplicationQuit()
    {
        SaverLoaderManager.instance.SaveGame();
    }
}