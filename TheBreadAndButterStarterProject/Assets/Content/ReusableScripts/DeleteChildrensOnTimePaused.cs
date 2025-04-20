using UnityEngine;

public class DeleteChildrensOnTimePaused : MonoBehaviour
{   
    void Start()
    {
        GameEventsManager.instance.OnTimePaused += gameObject.DeleteAllChildren;
    }

    void OnDestroy()
    {
        GameEventsManager.instance.OnTimePaused -= gameObject.DeleteAllChildren;
    }
}