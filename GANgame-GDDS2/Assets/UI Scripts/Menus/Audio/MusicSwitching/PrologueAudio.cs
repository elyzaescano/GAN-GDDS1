using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Stop("Menu");
        AudioManager.instance.Stop("Rain");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
