using UnityEngine;

public class DataAccessManager : MonoBehaviour
{
    public static DataAccessManager instance = null;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
