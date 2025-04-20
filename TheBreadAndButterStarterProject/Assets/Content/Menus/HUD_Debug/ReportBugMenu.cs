using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using DG.Tweening;
using System;

public class ReportBugMenu : MonoBehaviour
{
    [SerializeField] private GameObject reportBugMenuParent;
    [SerializeField] private TMP_Dropdown _reportTypeDropdown;
    [SerializeField] private TMP_InputField _reportDescriptionInput;
    [SerializeField] private TMP_Text _labelReportType;
    [SerializeField] private TMP_Text _labelDescription;
    // [SerializeField] private RawImage _screenshotPreview;
    // private byte[] _screenshotInBytes;
    private Color originalLabelColor;
    private const string googleFormsURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScegHN7mib7reHr3-k1W9PvDFXN161JfgNKHagtIkVXaDCmlw/formResponse";
    private const string inputReportTypeEntryID = "entry.530523009";
    private const string inputDescriptionEntryID = "entry.1170256860";
    private Tweener anim;
    // private const string inputScreenshotEntryID = "entry.1113848447";

    void Start()
    {
        // StartCoroutine(GetScreenshot());
        originalLabelColor = _labelDescription.color;
    }

    void OnDisable()
    {
        _labelDescription.transform.DOKill();
    }

    public void CancelButton()
    {
        reportBugMenuParent.SetActive(false);
        AudioManager.instance.coreSounds.PlayCloseMenuSound();
        UserInterfaceManager.instance.reportBugMenu.Close();
    }

    public void SubmitButton()
    {
        _labelDescription.color = originalLabelColor;
        
        if (string.IsNullOrEmpty(_reportDescriptionInput.text))
        {
            _labelDescription.color = Color.red;
            AudioManager.instance.coreSounds.PlayErrorSound();

            if (anim == null || !anim.IsActive())
                anim = _labelDescription.transform.DOShakePosition(0.3f, 5f, 25, 90, false).SetUpdate(true);

            return;
        }

        SubmitReportToGoogleForms();
    }
    
    private void SubmitReportToGoogleForms()
    {
        WWWForm form = new WWWForm();

        string reportType = _reportTypeDropdown.options[_reportTypeDropdown.value].text;

        form.AddField(inputReportTypeEntryID, reportType);
        form.AddField(inputDescriptionEntryID, reportType + "'\n'" + _reportDescriptionInput.text);
        
        // TODO: UPLOAD OF FILE FORCES GOOGLE FORM TO ASK FOR EMAIL AUTHENTICATION (THATS A BIG PROBLEM)
        // form.AddBinaryData(inputScreenshotEntryID, _screenshotInBytes, "screenshot.png", "image/png"); 

        StartCoroutine(SendWebRequestToGoogleForms(form, OnSubmitComplete));
    }
 
    private static IEnumerator SendWebRequestToGoogleForms(WWWForm form, Action callback) 
    {
        UnityWebRequest www = UnityWebRequest.Post(googleFormsURL, form);
        yield return www.SendWebRequest();

         if (www.result != UnityWebRequest.Result.Success)
            Debug.LogError( "Error while trying to submit bug/feedback report to Google Forms: " + www.error );
            
        callback.Invoke();
    }

    private void OnSubmitComplete()
    {
        reportBugMenuParent.SetActive(false);
        AudioManager.instance.coreSounds.PlaySuccessSound();
        UserInterfaceManager.instance.reportBugMenu.Close();
    }

    // private IEnumerator GetScreenshot()
    // {
    //     yield return new WaitForEndOfFrame();
        
    //     int width = Screen.width;
    //     int height = Screen.height;

    //     Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
    //     screenshotTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //     screenshotTexture.Apply();

    //     _screenshotPreview.texture = screenshotTexture;
    //     _screenshotInBytes = screenshotTexture.EncodeToPNG();
    // }
}