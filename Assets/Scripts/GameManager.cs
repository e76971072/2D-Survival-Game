using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    public static event Action OnGameLost = delegate { };
    public static event Action OnGameReload = delegate { };

    [HideInInspector] public Camera mainCamera;

    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        Time.timeScale = 1;
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
        UIManager.Instance.Restart();
    }

    public void GameLost()
    {
        playerInput.enabled = false;
        Time.timeScale = 0;
        StartCoroutine(UIManager.Instance.FadeLoseMenu());
        OnGameLost();
    }
}