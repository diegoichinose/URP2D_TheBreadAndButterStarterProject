using UnityEngine;

public class PropInstantiatorManager : MonoBehaviour
{
    public static PropInstantiatorManager instance = null;
    public Transform propsFolder;
        
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public GameObject InstantiateProp(GameObject prefab, Vector3 position, Transform parent = null)
    {
        prefab.SetActive(false);

        if (parent == null)
            parent = propsFolder;
            
        var prop = Instantiate(prefab, parent);
        prop.transform.SetPositionAndRotation(position, rotation: Quaternion.identity);
        
        // TODO: PERSIST PROCEDURALLY GENERATED PROPS HERE
        
        prop.SetActive(true);
        prefab.SetActive(true);
        return prop;
    }
}
