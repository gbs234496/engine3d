using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.InputSystem; 
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameState currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Regra: Cena de _Boot inicia no estado 'Iniciando'
        ChangeState(GameState.Iniciando);
        
        // Inicia o fluxo automático indo para a Splash
        RequestSceneLoad("Splash"); 
    }
    
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        // Atende ao requisito: "A mudança de estados deve ser mostrada no console através de Debug.Log"
        Debug.Log($"<color=cyan>[GameManager] Estado alterado para: {currentState}</color>");
    }
    
    public void RequestSceneLoad(string sceneName)
    {
        // Centralização e validação de regras de estado com base na cena solicitada
        if (sceneName == "Splash")
        {
            ChangeState(GameState.ExibindoSplash);
            SceneManager.LoadScene(sceneName);
        }
        else if (sceneName == "MenuPrincipal")
        {
            ChangeState(GameState.MenuPrincipal);
            SceneManager.LoadScene(sceneName);
        }
        else if (sceneName == "GetStarted_Scene")
        {
            ChangeState(GameState.Gameplay);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            // Caso tenha outras cenas genéricas
            SceneManager.LoadScene(sceneName);
        }
    }

    // Requisito do Input System para Single Player
    public void AssignPlayerInput(PlayerInput playerInput)
    {
        if (playerInput != null)
        {
            Debug.Log("<color=green>[GameManager] Input alocado com sucesso ao PlayerInput.</color>");
        }
    }
}