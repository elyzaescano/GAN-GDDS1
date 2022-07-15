using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitching : MonoBehaviour
{
   

    public void Start()
    {
       

    }
    public void PlayGame()
    {
        SceneManager.LoadScene("House1");


    }

    public void LoadSaves()
    {
        SceneManager.LoadScene("LoadGame");
    }

    

    public void Credits()
    {
        SceneManager.LoadScene("Credits");

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    public void Quit()
    {
        Application.Quit();
    }
}
