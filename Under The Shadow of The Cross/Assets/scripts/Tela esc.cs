using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Arraste o Panel do menu no Inspector
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        pausePanel.SetActive(true);   // Mostra o painel
        Time.timeScale = 0f;          // Congela o jogo
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);  // Esconde o painel
        Time.timeScale = 1f;          // Volta o jogo
        isPaused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;          // Descongela antes de trocar de cena
        SceneManager.LoadScene("Menu"); // Troca para a cena "Menu" (renomeie se precisar)
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();           // Fecha o jogo (funciona no build, não no editor)
    }
}