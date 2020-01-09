using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    public static event Action OnGameLost = delegate { };
    public static event Action OnGameReload = delegate { };

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

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;
        
        OnGameReload();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoseGame()
    {
        OnGameLost();
    }
}