using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("Lose Menu Canvas")]
        [SerializeField] private CanvasGroup loseMenuCanvas;
        [SerializeField] private float canvasFadeSpeed;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public IEnumerator FadeLoseMenu()
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
            SceneManager.LoadScene("Main Menu");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Level 01");
        }
    }
}