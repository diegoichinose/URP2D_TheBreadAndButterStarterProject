using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
 
public class FeedbackCollector : MonoBehaviour 
{
    private const string googleFormsURL = "https://docs.google.com/forms/d/e/1FAIpQLScegHN7mib7reHr3-k1W9PvDFXN161JfgNKHagtIkVXaDCmlw/formResponse";
    private const string inputTitleEntryID = "entry.1330939643";
    private const string inputDescriptionEntryID = "entry.1330939643";
    private const string inputScreenshotEntryID = "entry.1330939643";
 
    public void SendReportToGoogleForms(string title, string description, byte[] screenshot)
    {
        WWWForm form = new WWWForm();
        form.AddField(inputTitleEntryID, title);
        form.AddField(inputDescriptionEntryID, description);
        form.AddBinaryData(inputScreenshotEntryID, screenshot, "screenshot.png", "image/png");

        StartCoroutine(SendWebRequestToGoogleForms(form));
    }
 
    private static IEnumerator SendWebRequestToGoogleForms(WWWForm form) 
    {
        using (UnityWebRequest www = UnityWebRequest.Post(googleFormsURL, form)) 
        {
            yield return www.SendWebRequest();
        }
    }
}
 