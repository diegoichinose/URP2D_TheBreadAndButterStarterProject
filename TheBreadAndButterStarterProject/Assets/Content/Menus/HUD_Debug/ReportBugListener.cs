using UnityEngine;
using UnityEngine.InputSystem;

public class ReportBugListener : MonoBehaviour
{
    private GameInput _input;

    void Start()
    {
        _input = new GameInput();
        _input.Enable();
        _input.Debug.ReportBug.performed += OpenReportBugUrl;
        _input.Debug.GiveFeedback.performed += OpenGiveFeedbackUrl;
    }

    void OnDestroy()
    {
        _input.Debug.ReportBug.performed -= OpenReportBugUrl;
        _input.Debug.GiveFeedback.performed -= OpenGiveFeedbackUrl;
        _input.Disable();
    }

    private void OpenReportBugUrl(InputAction.CallbackContext context) 
    {
        Application.OpenURL(Constants.REPORT_BUG_URL);
    }

    private void OpenGiveFeedbackUrl(InputAction.CallbackContext context) 
    {
        Application.OpenURL(Constants.GIVE_FEEDBACK_URL);
    }
}