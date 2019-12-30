using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    public static event Action OnGameLost = delegate { };

    [HideInInspector] public Camera mainCamera;

    private void Awake()
    {
        OnGameLost = delegate { };
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        mainCamera = Camera.main;
    }

    public void LoseGame()
    {
        OnGameLost();
    }
}