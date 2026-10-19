
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoKills;

    private void Start()
    {
        if (textoKills != null)
        {
            textoKills.text =
                "Zumbis eliminados: " + KillCounter.TotalKills;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (KillCounter.Instance != null)
                KillCounter.Instance.ReiniciarContador();

            SceneManager.LoadScene("Fase1");
        }
    }
}
