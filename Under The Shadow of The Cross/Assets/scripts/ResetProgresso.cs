using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResetProgresso : MonoBehaviour
{
    public float delay = 1f; // Delay em segundos antes de resetar e voltar ao in�cio
    public string cenaInicial = "CenaInicial"; // Nome da cena inicial

    // Este m�todo deve ser chamado pelo bot�o
    public void ResetarJogo()
    {
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        // Aguarda o delay
        yield return new WaitForSeconds(delay);

        // Limpa todos os dados salvos
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Volta para a cena inicial
        SceneManager.LoadScene(cenaInicial);
    }
}
