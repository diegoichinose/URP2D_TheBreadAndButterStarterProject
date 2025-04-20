using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyCustomNonSelectableButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Color originalBackgroundColor;
    [SerializeField] private Color originalTextColor;
    [SerializeField] private Color onSelectBackgroundColor;
    [SerializeField] private Color onSelectTextColor;
    [SerializeField] private float onDeselectPositionOffsetX;
    [SerializeField] private float onDeselectPositionYOffsetY;
    [SerializeField] private Ease easing;
    [SerializeField] private float duration;
    private Vector3 originalLocalPosition;
    public UnityEvent OnSelect;

    void Awake()
    {
        originalLocalPosition = transform.localPosition;
        
        if (transform.parent.TryGetComponent<GridLayoutGroup>(out var _gridLayout))
            _gridLayout.enabled = false;
    }

    void OnDestroy()
    {
        transform.DOKill();
        OnSelect = null;
    }

    public void OnPointerClick(PointerEventData eventData) => Select();
    public void Select()
    {
        OnSelect.Invoke();
        GetComponent<Image>().color = onSelectBackgroundColor;
        buttonText.color = onSelectTextColor;

        transform.DOKill();
        transform.DOLocalMove(originalLocalPosition, duration).SetEase(easing).SetUpdate(true);
    }
    
    public void Deselect()
    {
        GetComponent<Image>().color = originalBackgroundColor;
        buttonText.color = originalTextColor;

        transform.DOKill();
        var deselectPosition = new Vector3(originalLocalPosition.x + onDeselectPositionOffsetX, originalLocalPosition.y + onDeselectPositionYOffsetY, 0);
        transform.localPosition = deselectPosition;
    }
}