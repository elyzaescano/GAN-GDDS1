using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitching : MonoBehaviour
{
    AudioManager audioManager;

    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("House1");
        audioManager.Stop("Menu");
        
    }

    public void LoadSaves()
    {
        SceneManager.LoadScene("LoadGame");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
        
        audioManager.Play("Menu");
        
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        audioManager.Play("Menu");
    }

}
