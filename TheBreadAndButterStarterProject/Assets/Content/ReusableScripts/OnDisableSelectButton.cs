using UnityEngine;
using UnityEngine.UI;

public class OnDisableSelectButton : MonoBehaviour
{
    [SerializeField] private Selectable button;

    void OnDisable() 
    {
        EventSystemManager.instance.InvokeAfterTime(() => 
        {
            if (button)
                button.Select();
                
        }, time: 0.1f);
    }
}