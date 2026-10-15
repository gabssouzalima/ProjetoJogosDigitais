using UnityEngine;
using UnityEngine.SceneManagement;

// Script simples com os 3 botões: Jogar, Jogar Novamente e Menu.
// Não depende de nenhum outro script.
public class BotoesTelas : MonoBehaviour
{
    // sem mexer no código. Têm que ser IGUAIS ao nome da cena no Unity.
    public string nomeCenaMenu = "TelaDeInicio";
    public string nomeCenaJogo = "Fase1";

    // Botão JOGAR (cena do menu)
    public void Jogar()
    {
        SceneManager.LoadScene(nomeCenaJogo);
    }

    // Botão JOGAR NOVAMENTE (cena de Game Over)
    public void JogarNovamente()
    {
        SceneManager.LoadScene(nomeCenaJogo);
    }

    // Botão MENU (cena de conclusão)
    public void VoltarAoMenu()
    {
        SceneManager.LoadScene(nomeCenaMenu);
    }
}
