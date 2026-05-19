using System;
using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{

    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }
    
}