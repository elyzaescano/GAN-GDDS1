using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Menu");
        AudioManager.instance.Stop("Tutorial");
        AudioManager.instance.Play("Rain");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
