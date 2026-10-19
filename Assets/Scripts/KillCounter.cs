
using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter Instance;

    public static int TotalKills { get; private set; }

    [SerializeField] private TextMeshProUGUI textoKills;

    void Awake()
    {
        Instance = this;
        TotalKills = 0;
    }

    void Start()
    {
        AtualizarTexto();
    }

    public void AdicionarKill()
    {
        TotalKills++;
        AtualizarTexto();
    }

    private void AtualizarTexto()
    {
        if (textoKills != null)
            textoKills.text = "Zombies: " + TotalKills;
    }

    public void ReiniciarContador()
    {
        TotalKills = 0;
        AtualizarTexto();
    }

}
