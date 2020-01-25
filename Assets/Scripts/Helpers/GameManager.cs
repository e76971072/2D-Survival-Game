using System;
using Player;
using UI;
using UnityEngine;

namespace Helpers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static event Action OnGameLost = delegate { };

        public Camera mainCamera;
        [HideInInspector] public GameObject player;
        public PlayerInput playerInput;


        private void Awake()
        {
            Time.timeScale = 1;
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            player = playerInput.gameObject;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.R)) return;

            UIManager.Instance.Restart();
        }

        public void GameLost()
        {
            playerInput.enabled = false;
            Time.timeScale = 0;
            OnGameLost();
            StartCoroutine(UIManager.Instance.FadeLoseMenu());
        }
    }
}