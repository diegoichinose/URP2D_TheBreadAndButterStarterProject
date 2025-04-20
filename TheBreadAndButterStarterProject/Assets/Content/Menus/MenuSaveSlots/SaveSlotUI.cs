using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class SaveSlotUI : MonoBehaviour
{
    [SerializeField] private GameObject saveFileInfo;
    [SerializeField] private GameObject emptySaveIcon;
    [SerializeField] private TMP_Text playTime;
    [SerializeField] private TMP_Text lastUpdatedTime;
    public int thisSaveSlotIndex;
    private SaveFile thisSaveSlotFile;
    private bool isSaveSlotEmpty;

    void OnEnable()
    {
        UpdateSaveSlotUI();
    }

    public void UpdateSaveSlotUI()
    {
        thisSaveSlotFile = null;

        if (SaverLoaderManager.instance.isLocalSaveFileLoaded)
            thisSaveSlotFile = SaverLoaderManager.instance.localSaveFileList.GetSaveFile(thisSaveSlotIndex);

        if (thisSaveSlotFile == null)
        {
            isSaveSlotEmpty = true;
            saveFileInfo.SetActive(false);
            emptySaveIcon.SetActive(true);
            return;
        }

        isSaveSlotEmpty = false;
        saveFileInfo.SetActive(true);
        emptySaveIcon.SetActive(false);

        TimeSpan playTimeSpan = TimeSpan.FromSeconds(thisSaveSlotFile.totalPlayTimeInSeconds);

        playTime.text = playTimeSpan.Hours + "h " + playTimeSpan.Minutes +  "m";
        lastUpdatedTime.text = thisSaveSlotFile.GetLastUpdated().ToString("dd MMM yyyy hh:mm tt");
    }

    public void SelectSaveSlotButton()
    {
        SaverLoaderManager.instance.TryLoadGame(thisSaveSlotIndex);
        SceneTransitionManager.instance.OnSaveSlotClicked(isSaveSlotEmpty);
    }
}