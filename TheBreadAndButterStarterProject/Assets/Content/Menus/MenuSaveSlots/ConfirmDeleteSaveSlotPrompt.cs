using UnityEngine;
using UnityEngine.UI;

public class ConfirmDeleteSaveSlotPrompt : MonoBehaviour
{
    [SerializeField] private TitleScreenButtons _titleScreenButtons;

    [Header("DO NOT TOUCH - FOR INSPECTION ONLY")]
    public SaveSlotUI saveSlotToDelete;
    public Button onDisableSelectThis;

    void OnEnable()
    {
        AudioManager.instance.coreSounds.PlayOpenMenuSound();
        _titleScreenButtons.UnsubscribeCloseSaveSlotMenuFromInput();
    }

    void OnDisable()
    {
        AudioManager.instance.coreSounds.PlayCloseMenuSound();
        _titleScreenButtons.SubscribeCloseSaveSlotMenuFromInput();
        onDisableSelectThis.Select();
    }

    public void ConfirmDeleteSaveSlotButton()
    {
        LoadingScreenManager.instance.Show("<color=yellow>Deleting Save Slot...</color><br>Purging memories and achievements");
        EventSystemManager.instance.RunNextFrame(() => 
        {
            AudioManager.instance.coreSounds.PlaySuccessSound();
            SaverLoaderManager.instance.DeleteSaveSlot(saveSlotToDelete.thisSaveSlotIndex);
            saveSlotToDelete.UpdateSaveSlotUI();
            gameObject.SetActive(false);
            LoadingScreenManager.instance.Hide();
        });
    }

    public void CancelButton()
    {
        gameObject.SetActive(false);
    }
}