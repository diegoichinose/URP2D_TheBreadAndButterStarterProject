using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;

[Serializable]
public class MyVfxLibrary
{
	public PlayableVfx dustVFX;
}

[Serializable]
public class PlayableVfx
{
	public AssetReferenceGameObject prefab;
	public void Play(Vector3 position) => VisualEffectsManager.instance.PlayVisualEffects(prefab, position);
	public void Play(Vector3 localPosition, Transform stickToParent) => VisualEffectsManager.instance.PlayVisualEffects(prefab, localPosition, optionalParent: stickToParent);
}

public class VisualEffectsManager : MonoBehaviour
{
    public static VisualEffectsManager instance = null;
	public MyVfxLibrary myVfxLibrary;
	[SerializeField] private UnityEngine.Rendering.Volume _postProcessing;
	[SerializeField] private Transform visualEffectsFolder;

	[Header("DO NOT TOUCH - FOR INSPECTION ONLY")]
	[SerializeField] private GameObjectPool visualEffectsPool;
	private UnityEngine.Rendering.Universal.Vignette _vignette;
	private UnityEngine.Rendering.Universal.ColorAdjustments _colorAdjustments;
    private Coroutine coroutine = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {   
		if(!_postProcessing.profile.TryGet(out _vignette)) 
			throw new System.NullReferenceException(nameof(_vignette));

		if(!_postProcessing.profile.TryGet(out _colorAdjustments)) 
			throw new System.NullReferenceException(nameof(_colorAdjustments));

		visualEffectsPool = new GameObjectPool();
		
		DisableTimeChamberPostProcessing();
    }

    void OnDestroy()
    {
		_vignette.active = false;
    }

	// -------------------------------------------------------------------------------------------- // 
	public void EnableRedVignetteBriefly()
	{
		_vignette.active = true;
		StartCoroutine(WaitAndDisableVignette(0.2f));
	}

	private IEnumerator WaitAndDisableVignette(float waitTime)
	{
		yield return new WaitForSeconds(waitTime); 
		DisableVignette();
	}

	public void EnableBlackAndWhiteBriefly()
	{
		_colorAdjustments.active = true;
		coroutine = StartCoroutine(WaitAndDisableBlackAndWhite(0.2f));
	}

	private IEnumerator WaitAndDisableBlackAndWhite(float waitTime)
	{
		yield return new WaitForSecondsRealtime(waitTime); 
        DisableBlackAndWhite();
	}
    
	public void EnableBlackAndWhite() { _colorAdjustments.active = true; if (coroutine != null) StopCoroutine(coroutine); }
	public void EnableTimeChamberPostProcessing() { _colorAdjustments.active = true; } 
	public void DisableTimeChamberPostProcessing() {  _colorAdjustments.active = false; } 

	public void DisableBlackAndWhite() => _colorAdjustments.active = false;
	public void DisableVignette() => _vignette.active = false;

	// -------------------------------------------------------------------------------------------- //
    public void PlayVisualEffects(AssetReferenceGameObject assetReference, Vector3 position, Transform optionalParent = null, Action<GameObject> OnSuccess = null)
    {	
		// LOAD ADDRESSABLE ASSET
        AddressablesManager.instance.Load(assetReference, OnSuccess: result => 
		{
			// CHECK IF INACTIVE SAME TYPE VFX EXIST IN POOL
			var poolUnit = visualEffectsPool.pool.FirstOrDefault(x => x.assetReference == assetReference && x.gameObjectInstance.activeSelf == false);
			if (poolUnit != null)
			{
				poolUnit.gameObjectInstance.transform.SetPositionAndRotation(position, Quaternion.identity);
				poolUnit.gameObjectInstance.SetActive(true);
				return;
			}

			// IF VALID POOL INSTANCE NOT FOUND, INSTANTIATE AND ADD TO POOL
			poolUnit = new GameObjectPoolUnit();
			poolUnit.assetReference = assetReference;
			poolUnit.gameObjectInstance = Instantiate(result, position, Quaternion.identity, visualEffectsFolder);

			if (optionalParent != null)
			{
				poolUnit.gameObjectInstance.transform.SetParent(optionalParent);
				poolUnit.gameObjectInstance.transform.localPosition = position;
			}

			// UNIVERSAL VFX SETUP: ENFORCE SORTING TO BE ALWAYS ON TOP
			if (poolUnit.gameObjectInstance.TryGetComponent<SortingGroup>(out var sorting) == false)
				sorting = poolUnit.gameObjectInstance.AddComponent<SortingGroup>();
			
			// UNIVERSAL VFX POOL SETUP: ENFORCE COMPONENT THAT LISTENS TO ONDESTROY
			if (poolUnit.gameObjectInstance.TryGetComponent<VisualEffectsGameObject>(out var vfx) == false)
				vfx = poolUnit.gameObjectInstance.AddComponent<VisualEffectsGameObject>();

			sorting.sortingLayerName = "Last";
			poolUnit.gameObjectInstanceId = vfx.Setup(OnDestroyCallback: RemoveFromPool);
			OnSuccess?.Invoke(poolUnit.gameObjectInstance);
			visualEffectsPool.pool.Add(poolUnit);
		});
	}

	// CALLED WHEN ANY POOL UNIT IS DESTROYED
	public void RemoveFromPool(SerializableGuid gameObjectInstanceId) 
	{
		visualEffectsPool.pool.RemoveAll(x => x.gameObjectInstanceId == gameObjectInstanceId);
	}

	// CALLED ON OPEN WORLD EXIT
	private void WipePool() 
	{
		foreach (var x in visualEffectsPool.pool.ToList())
		{	
			Destroy(x.gameObjectInstance);
		}

		visualEffectsPool = new GameObjectPool();
	}
}

[Serializable]
public class GameObjectPool
{
	public List<GameObjectPoolUnit> pool;

	public GameObjectPool()
	{
		pool = new List<GameObjectPoolUnit>();
	}
}

[Serializable]
public class GameObjectPoolUnit
{
	public AssetReferenceGameObject assetReference;
	public SerializableGuid gameObjectInstanceId;
	public GameObject gameObjectInstance;
}

public enum TargetType
{
	None,
	Player,
	Enemy,
	DamageSourceEnemy,
}

public enum TriggerSource
{
    None,
    OnDash,
    OnPlayerHit,
    OnEnemyHit,
    EveryFewSeconds,
}