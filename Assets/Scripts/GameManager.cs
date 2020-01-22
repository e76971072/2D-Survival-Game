using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action OnGameLost = delegate { };
    public static event Action OnGameReload = delegate { };

    public Camera mainCamera;

    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        Time.timeScale = 1;
        OnGameLost = delegate { };
        OnGameReload = delegate { };
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
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