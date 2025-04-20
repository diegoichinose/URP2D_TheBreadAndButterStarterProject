using TMPro;
using UnityEngine;

public class ConsoleTextPrinter : MonoBehaviour
{
    [SerializeField] private TMP_Text consoleText;
    [SerializeField] private int logMaxLength = 700;
    [SerializeField] private string filename = "";
    private string myLog;

    void OnEnable()
    {
        consoleText.text = "no logs to show";
        Application.logMessageReceived += PrintLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= PrintLog;
    }

    public void ClearLog()
    {
        myLog = "";
        consoleText.text = "no logs to show";
    }

    public void PrintLog(string logString, string stackTrace, LogType type)
    {
        myLog += logString + "<br>";

        if (myLog.Length > logMaxLength)
            myLog = myLog.Substring(myLog.Length - logMaxLength); 

        if (type is LogType.Error)
        if (string.IsNullOrEmpty(stackTrace) == false)
            myLog += " // stacktrace: " + stackTrace;

        consoleText.text = myLog;

    }

    private void PrintLogToFile(string logString)
    {
        if (filename == "")
        {
            string d = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/YOUR_LOGS";
            System.IO.Directory.CreateDirectory(d);

            string r = Random.Range(1000, 9999).ToString();
            filename = d + "/log-" + r + ".txt";
        }

        try 
        { 
            System.IO.File.AppendAllText(filename, logString + ""); 
        }
        catch { }
    }
}