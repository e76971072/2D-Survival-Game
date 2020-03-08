using System;
using Data;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static event Action OnGameLost;

        public Camera mainCamera;
        public PlayerInput playerInput;
        [HideInInspector] public GameObject player;
        
        private void Awake()
        {
            Time.timeScale = 1;
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            player = playerInput.gameObject;
        }

        public void GameLost()
        {
            playerInput.enabled = false;
            Time.timeScale = 0;
            OnGameLost?.Invoke();
        }
    }
}