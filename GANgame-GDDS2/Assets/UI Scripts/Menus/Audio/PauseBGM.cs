using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "House1")
        {
            AudioManager.instance.GetComponent<AudioSource>().Pause();
        }
        if (SceneManager.GetActiveScene().name == "Diary Test")
        {
            AudioManager.instance.GetComponent<AudioSource>().Pause();
        }
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            AudioManager.instance.GetComponent<AudioSource>().Pause();
        }
    }
}
