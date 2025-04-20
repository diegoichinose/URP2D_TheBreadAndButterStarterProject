using System.Collections.Generic;
using UnityEngine;

public class PathHelper
{
    int randomPath;
    List<int> usedPaths = new List<int>();

    public Vector2 GetRandomNearbyPosition(Vector2 origin, float range = 0.5f) => GetRandomFromListWihoutRepeating(GetNearbyPositions(origin, range));

    public List<Vector2> GetNearbyPositions(Vector2 origin, float range = 0.5f)
    {
        var rangeInbetween = range - 0.2f;
        List<Vector2> list = new List<Vector2>();
        list.Add(new Vector2(origin.x + 0f,                 origin.y + 0f));
        list.Add(new Vector2(origin.x + rangeInbetween,     origin.y + 0f));
        list.Add(new Vector2(origin.x + -rangeInbetween,    origin.y + 0f));
        list.Add(new Vector2(origin.x + range,              origin.y + 0f));
        list.Add(new Vector2(origin.x + -range,             origin.y + 0f));
        list.Add(new Vector2(origin.x + 0f,                 origin.y + rangeInbetween));
        list.Add(new Vector2(origin.x + 0f,                 origin.y + -rangeInbetween));
        list.Add(new Vector2(origin.x + 0f,                 origin.y + range));
        list.Add(new Vector2(origin.x + 0f,                 origin.y + -range));
        list.Add(new Vector2(origin.x + rangeInbetween,     origin.y + rangeInbetween));
        list.Add(new Vector2(origin.x + range,              origin.y + range));
        list.Add(new Vector2(origin.x + -rangeInbetween,    origin.y + -rangeInbetween));
        list.Add(new Vector2(origin.x + -range,             origin.y + -range));
        return list;
    }

    public Vector2 GetRandomFromListWihoutRepeating(List<Vector2> list) 
    {
        if (usedPaths.Count == list.Count)
            usedPaths = new List<int>();

        do {
            randomPath = Random.Range(0, list.Count);
        } while (usedPaths.Contains(randomPath));
        usedPaths.Add(randomPath);

        return list[randomPath];
    }

    public Vector3 GetRandomFromListWihoutRepeating(GameObject[] list) 
    {
        if (usedPaths.Count == list.Length)
            usedPaths = new List<int>();

        do {
            randomPath = Random.Range(0, list.Length);
        } while (usedPaths.Contains(randomPath));
        usedPaths.Add(randomPath);

        return list[randomPath].transform.position;
    }
}