using System.Collections.Generic;
using UnityEngine;

public class InteractionTooltipManager : MonoBehaviour
{
    public static InteractionTooltipManager instance = null;
    [SerializeField] private GameObject interactionTooltipPrefab;
    [SerializeField] private GameObject progressBarTooltipPrefab;
    [SerializeField] private float positionOffsetY;
    
    [Header("DO NOT TOUCH - FOR INSPECTION ONLY")]
    [SerializeField] private Dictionary<GameObject, GameObject> instantiatedTooltips; // KEY is the source, VALUE is the tooltip

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        instantiatedTooltips = new Dictionary<GameObject, GameObject>();
        interactionTooltipPrefab.SetActive(false);
        progressBarTooltipPrefab.SetActive(false);
    }

    void OnDestroy()
    {
        interactionTooltipPrefab.SetActive(true);
        progressBarTooltipPrefab.SetActive(true);
    }

    public void ShowInteractionTooltip(GameObject source) => ShowTooltip(source, interactionTooltipPrefab);
    public ProgressBar ShowProgressBar(GameObject source) => ShowTooltip(source, progressBarTooltipPrefab).GetComponent<ProgressBar>();
    private GameObject ShowTooltip(GameObject source, GameObject tooltipPrefab)
    {
        var tooltipGameObject = Instantiate(tooltipPrefab);

        // SET TOOLTIP POSITION TO THE SOURCE POSITION
        var harvestablePropPosition = source.transform.position;
        tooltipGameObject.transform.position = new Vector3(harvestablePropPosition.x, harvestablePropPosition.y + positionOffsetY, 0);
        tooltipGameObject.SetActive(true);

        // DESTOY ANY EXISTING TOOLTIP WITH THIS SOURCE
        if (instantiatedTooltips.ContainsKey(source))
        {
            Destroy(instantiatedTooltips.GetValueOrDefault(source));
            instantiatedTooltips.Remove(source);
        }
        
        // SAVE THE TOOLTIP TO BE REMOVED LATER
        instantiatedTooltips.Add(source, tooltipGameObject);

        return tooltipGameObject;
    }

    public void HideTooltip(GameObject source)
    {
        if (instantiatedTooltips.TryGetValue(source, out var tooltip) == false)
            return;

        Destroy(tooltip);
        instantiatedTooltips.Remove(source);
    }
}
