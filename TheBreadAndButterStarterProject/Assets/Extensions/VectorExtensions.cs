using System;
using UnityEngine;

public static class VectorExtensions
{
    public static bool CompareTo(this Vector2 self, Vector2 target)  
    {  
        if (Math.Round(self.x, 3) == Math.Round(target.x, 3))
        if (Math.Round(self.y, 3) == Math.Round(target.y, 3))
            return true;
            
        return false;
    }

    public static Vector3Int ToVector3Int(this Vector2 self)  
    {  
        return new Vector3Int((int) self.x, (int) self.y);
    }
    
    public static Vector2Int ToVector2Int(this Vector2 self)  
    {  
        return new Vector2Int((int) self.x, (int) self.y);
    }
    
    public static Vector2 ToVector2(this Vector3Int self)  
    {  
        return new Vector2(self.x, self.y);
    }

    public static Vector3 GetSum(this Vector3 self, float valueToSum)
    {  
        return new Vector3(self.x + valueToSum, self.y + valueToSum, self.z + valueToSum);
    }
}
