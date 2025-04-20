using UnityEngine;
using UnityEngine.UI;

public class SetFireInputIcon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable() 
    { 
        SetIcon(); 
        InputDetectionManager.instance.OnInputDeviceChange += SetIcon; 
    } 

    void OnDisable()
    {
        InputDetectionManager.instance.OnInputDeviceChange -= SetIcon; 
    } 

    private void SetIcon() 
    {
        if (image) 
            image.sprite = InputDetectionManager.instance.currentIconData.fire1; 

        if (spriteRenderer) 
            spriteRenderer.sprite = InputDetectionManager.instance.currentIconData.fire1;
    }
}