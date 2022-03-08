using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadHostMenu()
    {
        SceneManager.LoadScene("Host Menu");
    }

    public void LoadServerBrowser(){
        SceneManager.LoadScene("Server Browser");
    }

    public void LoadSettings(){
        SceneManager.LoadScene("Settings");
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit(){
        SceneManager.LoadScene("Quit Screen");
    }
}
