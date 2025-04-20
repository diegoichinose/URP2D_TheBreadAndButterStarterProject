using UnityEngine;

public class OnDisableSetAnimatorToDefaultState : MonoBehaviour
{
    private void OnDisable()
    {
        GetComponent<Animator>().Play("DefaultState");
    }
}