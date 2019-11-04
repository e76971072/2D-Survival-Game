using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Camera mainCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }
}