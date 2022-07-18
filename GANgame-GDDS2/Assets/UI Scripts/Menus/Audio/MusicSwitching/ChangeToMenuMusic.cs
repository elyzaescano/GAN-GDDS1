using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Menu");
        AudioManager.instance.Stop("Level");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
