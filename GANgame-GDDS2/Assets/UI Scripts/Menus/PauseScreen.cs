using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseScreen : MonoBehaviour
{
    public GameObject background;
    public bool canMove;
    public AudioSource menuButton;

    

    public void Resume()
    {
        Time.timeScale = 1f; //Resume game
        background.SetActive(false);
        menuButton.Play();
        AudioManager.instance.UnPause("Level");
        canMove = true;
    }

   public void Pause()
    {
        Time.timeScale = 0f; //Freeze game
        background.SetActive(true);
        menuButton.Play();
        AudioManager.instance.Pause("Level");
        canMove = false;
    }

   public void BacktoMenu()
    {
        menuButton.Play();
        SceneManager.LoadScene("Menu");
        Resume();
    }

    public void Quit()
    {
        menuButton.Play();
        Application.Quit();
    }

    
}
