using UnityEngine;

public class GameplaySpeed : MonoBehaviour
{
    public void SetSpeed(int amount) => Time.timeScale = amount;
}