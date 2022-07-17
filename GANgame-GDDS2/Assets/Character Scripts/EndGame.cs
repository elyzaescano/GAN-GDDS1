using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : LockDoor
{
    public string sceneToMoveTo;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!isLocked)
        {
            SceneManager.LoadScene(sceneToMoveTo);
        }
    }
}
