using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Configuração dos Botões")]
    [SerializeField] private Button btnIniciar;
    [SerializeField] private Button btnSair;

    private void Start()
    {
        if (btnIniciar != null)
            btnIniciar.onClick.AddListener(IniciarJogo);

        if (btnSair != null)
            btnSair.onClick.AddListener(SairDoJogo);
    }

    public void IniciarJogo()
    {
        if (GameManager.Instance != null)
        {
            // Requisito: Botão Iniciar deve carregar a cena GetStarted_Scene
            GameManager.Instance.RequestSceneLoad("GetStarted_Scene");
        }
    }

    public void SairDoJogo()
    {
        Debug.Log("O jogo fecharia agora (Application.Quit)");
        Application.Quit();
    }
}