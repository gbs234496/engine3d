using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour
{
    private void Start()
    {
        // Inicia a contagem de tempo assim que a cena carrega
        StartCoroutine(ContagemSplash());
    }

    private IEnumerator ContagemSplash()
    {
        // Requisito: Exibida por 2 segundos
        yield return new WaitForSeconds(2f);
        
        // Solicita a mudança para o GameManager (único com acesso ao SceneManager)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RequestSceneLoad("MenuPrincipal");
        }
    }
}