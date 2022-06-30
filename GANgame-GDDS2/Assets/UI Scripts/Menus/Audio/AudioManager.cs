using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    
       public static AudioManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject); //If another instance of BGM gameobject exists in another scene, destroy
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //If it doesnt, dont destroy on load
        }
    }




}
 