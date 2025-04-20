using UnityEngine;

public static class GameObjectExtensions
{
    public static void DeleteAllChildren(this GameObject thisGameObject)
    {
        foreach (Transform children in thisGameObject.transform)
        {
            GameObject.Destroy(children.gameObject);
        }
    }

    public static void DeleteAllChildren(this GameObject thisGameObject, float timer)
    {
        foreach (Transform children in thisGameObject.transform)
        {
            GameObject.Destroy(children.gameObject, timer);
        }
    }
}