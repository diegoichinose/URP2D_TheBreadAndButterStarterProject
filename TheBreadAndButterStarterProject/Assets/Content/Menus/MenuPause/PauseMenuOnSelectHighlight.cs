using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuOnSelectHighlight : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _description;
    [SerializeField] private RectTransform _title;
    private Button _button;

    void Start() => _button = GetComponent<Button>();
    void OnDisable() => DisableHighlight();

    private void EnableHighlight()
    {
        _highlight.SetActive(true);
        _description.SetActive(true);
        _title.localPosition = new Vector2(0, 15);
    }

    private void DisableHighlight()
    {
        _highlight.SetActive(false);
        _description.SetActive(false);
        _title.localPosition = new Vector2(0, 0);
    }

    public void OnSelect(BaseEventData eventData) 
    {
        EnableHighlight();
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
    }

    public void OnDeselect(BaseEventData eventData) => DisableHighlight();
    public void OnPointerEnter(PointerEventData eventData) => _button.Select();
}