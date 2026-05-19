/*
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Iniciando,
        MenuPrincipal,
        Gameplay
    }

    public GameState currentState;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Escuta quando a cena muda
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetState(GameState.Iniciando);
        LoadScene("splash");
    }

    // Chamado automaticamente quando uma cena carrega
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Cena carregada: " + scene.name);

        if (scene.name == "splash")
        {
            SetState(GameState.Iniciando);
        }
        else if (scene.name == "Menu")
        {
            SetState(GameState.MenuPrincipal);
        }
        else if (scene.name == "GetStarted_Scene")
        {
            SetState(GameState.Gameplay);
        }
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Estado atual: " + currentState);
    }

    // Controle de cenas (SÓ o GameManager pode fazer isso)
    public void LoadScene(string menuPrincipal)
    {
        SceneManager.LoadScene(menuPrincipal);
    }

    // Input allocation (simples)
    public void SetupPlayerInput(PlayerInput playerInput)
    {
        Debug.Log("Input atribuído ao jogador: " + playerInput.name);
    }

    public void ChangeState(global::GameState exibindoSplash)
    {
        throw new System.NotImplementedException();
    }

    public void RequestSceneLoad(string menuprincipal)
    {
        throw new System.NotImplementedException();
    }
}
*/
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
        ChangeState(GameState.Iniciando);
        RequestSceneLoad("Splash"); 
    }
    
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"<color=cyan>Estado do Jogo alterado para: {currentState}</color>");
    }
    
    public void RequestSceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    
    public void AssignPlayerInput(PlayerInput playerInput)
    {
        Debug.Log("Input alocado para o jogador.");
    }
    
    public void MenuPrincipal()
    {
        Instance.ChangeState(GameState.MenuPrincipal);
    }
    
    public void Splash()
    {
        Instance.ChangeState(GameState.ExibindoSplash);
    }
    
}
