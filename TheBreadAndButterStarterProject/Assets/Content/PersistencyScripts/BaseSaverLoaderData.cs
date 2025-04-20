using System;
using UnityEngine;

[Serializable]
public class BaseSaverLoaderData : ScriptableObject
{
    public virtual void OnNewSave(){}
    
    public virtual void SaveData(SaveFile saveFile)
    {
        // INHERIT AND OVERRIDE WITH YOUR OWN CODE TO FEED SaveFile
        // Eg. CREATE PlayerSaverLoaderData TO SAVE PLAYER DATA
    }
    
    public virtual void LoadData(SaveFile saveFile)
    {
        // INHERIT AND OVERRIDE WITH YOUR OWN CODE TO READ SaveFile
    }
}