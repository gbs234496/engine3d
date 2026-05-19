using System.Threading.Tasks; // Necessário para usar o Task.Delay
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoLoadBootScene
{
    private const string PrefKey = "LastOpenedScenePath";
    private const string BootSceneName = "_Boot";

    static AutoLoadBootScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    // A assinatura foi alterada para 'async void' para permitir pausas assíncronas
    private static async void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            string currentScenePath = SceneManager.GetActiveScene().path;
            EditorPrefs.SetString(PrefKey, currentScenePath);

            string[] guids = AssetDatabase.FindAssets(BootSceneName + " t:Scene");
            if (guids.Length > 0)
            {
                string bootPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                SceneAsset bootScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(bootPath);
                
                EditorSceneManager.playModeStartScene = bootScene;
            }
            else
            {
                Debug.LogWarning($"Cena '{BootSceneName}' não encontrada no projeto. Verifique o nome.");
            }
        }
        else if (state == PlayModeStateChange.EnteredPlayMode)
        {
            string lastScenePath = EditorPrefs.GetString(PrefKey, "");

            if (!string.IsNullOrEmpty(lastScenePath))
            {
                if (!lastScenePath.Contains(BootSceneName) && SceneManager.GetActiveScene().name == BootSceneName)
                {
                    // Aguarda 2 segundos (2000 milissegundos) sem congelar o Editor
                    await Task.Delay(2000);

                    // Uma verificação de segurança: garante que o Play Mode ainda está ativo.
                    // Isso evita erros caso você aperte o botão "Stop" antes dos 2 segundos passarem.
                    if (EditorApplication.isPlaying)
                    {
                        // Carrega a cena original e descarrega a _Boot
                        SceneManager.LoadScene(lastScenePath, LoadSceneMode.Additive);
                    }
                }
            }
        }
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            EditorSceneManager.playModeStartScene = null;
        }
    }
}