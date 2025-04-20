using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager instance = null;
    [SerializeField] private GameObject containerToEnableDisable;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private Image loadingFill;
    private string defaultLoadingText = "Loading...";

    void Start()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
            
        Hide();
    }

    public void Show(string optionalText = "")
    {
        AudioManager.instance.coreSounds.PlayLoadingScreenSound();
        loadingText.text = String.IsNullOrEmpty(optionalText) ? defaultLoadingText : optionalText;
        containerToEnableDisable.SetActive(true);
        StartCoroutine(SlowlyFillCosmeticLoading());
    }

    public void Hide()
    {
        AudioManager.instance.coreSounds.StopLoadingScreenSound();
        containerToEnableDisable.SetActive(false);
        StopAllCoroutines();
    }

    private IEnumerator SlowlyFillCosmeticLoading()
    {
        loadingFill.fillAmount = 0;
        while (loadingFill.fillAmount < 1)
        {
            loadingFill.fillAmount += 0.1f;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}