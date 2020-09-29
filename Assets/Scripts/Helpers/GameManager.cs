using Data;
using Player;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Helpers
{
    public class GameManager : MonoBehaviour
    {
        public Camera mainCamera;
        public PlayerInput playerInput;
        [HideInInspector] public GameObject player;

        [Inject] private readonly SignalBus _signalBus;
        
        private void Awake()
        {
            Time.timeScale = 1;
            player = playerInput.gameObject;
        }

        public void GameLost()
        {
            playerInput.enabled = false;
            Time.timeScale = 0;
            _signalBus.Fire<GameLostSignal>();
        }
    }
}