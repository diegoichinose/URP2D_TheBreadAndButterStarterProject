using System.Collections;
using TMPro;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public TMP_Text fpsText;
    public int targetFrameRate;
    private float fps;
 
    void OnEnable()
    {
        if (targetFrameRate > 0)
            Application.targetFrameRate = targetFrameRate;

        StartCoroutine(UpdateFramesPerSecondUI());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
     
    private IEnumerator UpdateFramesPerSecondUI()
    {
        while (true)
        {
            fps = 1f / Time.unscaledDeltaTime;
            fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}