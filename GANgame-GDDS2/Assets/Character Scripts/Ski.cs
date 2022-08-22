using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Ski : MonoBehaviour
{
    public PlayableDirector _currentDirector;
    bool _sceneSkipped = false;
    float timeToSkipTo;

   public void Skip (string sceneToLoad)
   {
        _currentDirector.Pause();
        SceneManager.LoadScene(sceneToLoad);
   }
}
