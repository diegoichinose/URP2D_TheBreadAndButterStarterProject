using System.Collections;
using UnityEngine;

public class FlashSpriteEffect : MonoBehaviour
{
    [SerializeField] private Material flashColor;
    [SerializeField] private Material flashColorCritical;
    [SerializeField] private SpriteRenderer _sprite;
    private float flashDurationSeconds = 0.15f;
    private Coroutine flashRoutine;
    private Material originalMaterialCache;
    
    void OnDisable()
    {
        StopAllCoroutines();
        flashRoutine = null;
        _sprite.material = originalMaterialCache;
    }

    void Start()
    {
        originalMaterialCache = _sprite.material;
    }

    public void FlashSpriteBriefly(bool isCriticalHit)
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);
            
        if (isCriticalHit)
            _sprite.material = flashColorCritical;
        else
            _sprite.material = flashColor;

        flashRoutine = StartCoroutine(WaitAndTurnSpriteBackToNormal());
    }

    private IEnumerator WaitAndTurnSpriteBackToNormal()
    {
        yield return new WaitForSeconds(flashDurationSeconds);
        flashRoutine = null;
        
        _sprite.material = originalMaterialCache;
    }
}