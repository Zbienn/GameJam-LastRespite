using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Sair do jogo (apenas funciona na build)");
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }
}
