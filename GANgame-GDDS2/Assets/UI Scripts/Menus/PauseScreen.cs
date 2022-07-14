using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseScreen : MonoBehaviour
{
    public GameObject background;
    public bool canMove;

    AudioManager audioManager;
    


    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 0)
            {
                Resume();
                EventManager.PauseScreen -= Resume;
            }
            else
            {
                Pause();
                EventManager.PauseScreen += Pause;
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f; //Resume game
        background.SetActive(false);
        audioManager.GetComponent<AudioSource>().Play();
        canMove = true;
    }

   public void Pause()
    {
        Time.timeScale = 0f; //Freeze game
        background.SetActive(true);
        audioManager.GetComponent<AudioSource>().Stop();
        canMove = false;
    }

   public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    
}
