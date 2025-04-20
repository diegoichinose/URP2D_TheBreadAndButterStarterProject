using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Scriptable Objects/Data List/Saver Loader Data List")]
public class SaverLoaderDataList : ScriptableObject
{
    public List<BaseSaverLoaderData> list;
}