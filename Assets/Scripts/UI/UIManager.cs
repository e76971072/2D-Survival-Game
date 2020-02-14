using System.Collections;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private CanvasGroup loseMenuCanvas;
        [SerializeField] private float canvasFadeSpeed;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
            GameManager.OnGameLost += LoseMenuHandler;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.R)) return;

            Restart();
        }

        private void LoseMenuHandler()
        {
            StartCoroutine(FadeLoseMenu());
        }

        private IEnumerator FadeLoseMenu()
        {
            while (loseMenuCanvas.alpha < 1)
            {
                loseMenuCanvas.alpha += Time.unscaledDeltaTime * canvasFadeSpeed;
                yield return null;
            }

            foreach (var button in loseMenuCanvas.GetComponentsInChildren<Button>()) button.interactable = true;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            SceneManager.LoadSceneAsync("Main Menu");
        }

        public void StartGame()
        {
            SceneManager.LoadSceneAsync("Level 01");
        }

        private void OnDestroy()
        {
            GameManager.OnGameLost -= LoseMenuHandler;
        }
    }
}