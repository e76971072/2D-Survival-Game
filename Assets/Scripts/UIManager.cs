using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance => _instance;

    [Header("Lose Menu Canvas")]
    [SerializeField] private float canvasFadeSpeed;
    [SerializeField] private CanvasGroup loseMenuCanvas;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public IEnumerator FadeLoseMenu()
    {
        while (loseMenuCanvas.alpha < 1)
        {
            loseMenuCanvas.alpha += Time.unscaledDeltaTime * canvasFadeSpeed;
            yield return null;
        }

        foreach (var button in loseMenuCanvas.GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }
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
        SceneManager.LoadScene("Scenes/Level 01");
    }
}