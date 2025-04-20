using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveFile
{
    // NOTES:
    // Usage of [SerializeReference] on lists are very important to persist abstraction (eg. InventoryItemWeapon continues to be a weapon on savefile and not just an item)
    public int saveSlotIndex;
    public float totalPlayTimeInSeconds;
    public string lastUpdated;
    public DateTime GetLastUpdated() => JsonUtility.FromJson<JsonDateTime>(lastUpdated);
    
    public SaveFile(int saveSlotIndex)
    {
        this.saveSlotIndex = saveSlotIndex;
        
        totalPlayTimeInSeconds = 0;
        lastUpdated = JsonUtility.ToJson((JsonDateTime) DateTime.Now);
    }
}