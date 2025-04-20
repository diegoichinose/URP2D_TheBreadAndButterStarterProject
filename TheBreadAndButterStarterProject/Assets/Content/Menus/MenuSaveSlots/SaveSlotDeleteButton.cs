using UnityEngine;
using UnityEngine.UI;

public class SaveSlotDeleteButton : MonoBehaviour
{
    [SerializeField] private SaveSlotUI _saveSlotUI;
    [SerializeField] private ConfirmDeleteSaveSlotPrompt _confirmDeleteSavePrompt;

    public void OpenDeleteSaveConfirmationPrompt()
    {
        _confirmDeleteSavePrompt.saveSlotToDelete = _saveSlotUI;
        _confirmDeleteSavePrompt.onDisableSelectThis = GetComponent<Button>();
        _confirmDeleteSavePrompt.gameObject.SetActive(true);
    }
}