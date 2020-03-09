using System.Collections;
using Data;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private CanvasGroup loseMenuCanvas;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI losingReasonText;
        [SerializeField] private float canvasFadeSpeed;

        private Button[] buttonArray;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            buttonArray = loseMenuCanvas.GetComponentsInChildren<Button>();
            
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
            highScoreText.text = Score.LoadHighScore().ToString();
            while (loseMenuCanvas.alpha < 1)
            {
                loseMenuCanvas.alpha += Time.unscaledDeltaTime * canvasFadeSpeed;
                yield return null;
            }

            foreach (var button in buttonArray) button.interactable = true;
        }

        public void SetLosingReasonText(string reason)
        {
            losingReasonText.text = reason;
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

        private void OnDestroy()
        {
            GameManager.OnGameLost -= LoseMenuHandler;
        }
    }
}