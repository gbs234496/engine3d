using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoLoadBootScene
{
    private const string PREF_KEY = "LastOpenedScenePath";
    private const string BOOT_SCENE_NAME = "_Boot";

    // O construtor estático é chamado automaticamente pelo Unity ao compilar graças ao [InitializeOnLoad]
    static AutoLoadBootScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // Momento em que o botão Play é pressionado, mas o jogo ainda não começou
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            // Salva o caminho da cena que estava aberta no Editor
            string currentScenePath = EditorSceneManager.GetActiveScene().path;
            EditorPrefs.SetString(PREF_KEY, currentScenePath);

            // Encontra a cena _Boot no projeto
            string[] guids = AssetDatabase.FindAssets(BOOT_SCENE_NAME + " t:Scene");
            if (guids.Length > 0)
            {
                string bootPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                SceneAsset bootScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(bootPath);
                
                // Força a cena _Boot a ser a cena inicial do Play Mode
                EditorSceneManager.playModeStartScene = bootScene;
            }
            else
            {
                Debug.LogWarning($"Cena '{BOOT_SCENE_NAME}' não encontrada no projeto. Verifique o nome.");
            }
        }
        // Momento em que o Play Mode de fato inicia e a cena _Boot já está rodando
        else if (state == PlayModeStateChange.EnteredPlayMode)
        {
            string lastScenePath = EditorPrefs.GetString(PREF_KEY, "");

            if (!string.IsNullOrEmpty(lastScenePath))
            {
                // Verifica se a cena inicial não era a própria _Boot (para evitar loop/duplicação)
                if (!lastScenePath.Contains(BOOT_SCENE_NAME) && SceneManager.GetActiveScene().name == BOOT_SCENE_NAME)
                {
                    // Carrega a cena que estava aberta de forma aditiva
                    SceneManager.LoadScene(lastScenePath, LoadSceneMode.Additive);

                    // Descarrega a cena _Boot
                    SceneManager.UnloadSceneAsync(BOOT_SCENE_NAME);
                }
            }
        }
        // Limpeza ao sair do Play Mode
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            // Remove a trava da cena inicial para não interferir em outros processos no futuro
            EditorSceneManager.playModeStartScene = null;
        }
    }
}