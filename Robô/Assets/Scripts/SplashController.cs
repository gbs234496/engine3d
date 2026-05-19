/*
using UnityEngine;

public class SplashController : MonoBehaviour
{
    private void Start()
    {
        Invoke("GoToMenu", 2f);
    }

    void GoToMenu()
    {
        GameManager.Instance.LoadScene("Menu");
    }
}
*/
using UnityEngine;
using System.Collections;

public class ExibindoSplash : MonoBehaviour
{
    IEnumerator Start()
    {
        
        yield return new WaitForSeconds(2f); // Espera 2 segundos
        
        
        GameManager.Instance.RequestSceneLoad("MenuPrincipal");
    }
}


