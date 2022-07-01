using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : LockDoor
{
    public Scene sceneToMoveTo;
    private void Update()
    {
        if (!isLocked)
        {
            SceneManager.LoadScene(1);
        }
    }
}
