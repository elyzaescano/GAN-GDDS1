using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitching : MonoBehaviour
{
    public string playScene;
    public string creditsScene;
    public string mainMenuScene;
    public AudioSource buttonSound;

    public void Start()
    {


    }
    public void PlayGame()
    {
        buttonSound.Play();
        SceneManager.LoadScene(playScene);

    }





    public void Credits()
    {
        buttonSound.Play();
        SceneManager.LoadScene(creditsScene);

    }

    public void ReturnToMainMenu()
    {
        buttonSound.Play();
        SceneManager.LoadScene(mainMenuScene);

    }

    public void Quit()
    {
        buttonSound.Play();
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

}
