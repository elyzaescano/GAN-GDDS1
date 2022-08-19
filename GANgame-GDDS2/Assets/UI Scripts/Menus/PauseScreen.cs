using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseScreen : MonoBehaviour
{
    public bool isPaused;
    public GameObject background;
    public bool canMove;
    public AudioSource menuButton;
    public GameObject loseScreen;
    public bool youLost;

    private void Update() 
    {
        if (loseScreen.activeInHierarchy == true)
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f; //Resume game
        background.SetActive(false);
        menuButton.Play();
        AudioManager.instance.UnPause("Level");
        canMove = true;
        isPaused = false;
    }

   public void Pause()
    {
        Time.timeScale = 0f; //Freeze game
        background.SetActive(true);
        menuButton.Play();
        AudioManager.instance.Pause("Level");
        canMove = false;
        isPaused = true;
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

    public void TryAgain()
    {
        loseScreen.SetActive(false);
        youLost = false;
        menuButton.Play();
        AudioManager.instance.Play("Level");
    }
    
}
