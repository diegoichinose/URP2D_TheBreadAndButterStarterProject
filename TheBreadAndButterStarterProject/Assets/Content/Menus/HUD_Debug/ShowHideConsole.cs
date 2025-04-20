using UnityEngine;

public class ShowHideConsole : MonoBehaviour
{
    [SerializeField] private GameObject consoleWindow;

    public void ToggleConsoleWindow()
    {
        consoleWindow.SetActive(!consoleWindow.activeSelf);
    }
}