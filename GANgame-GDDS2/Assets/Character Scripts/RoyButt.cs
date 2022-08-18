using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoyButt : MonoBehaviour
{
    public void ActiveSetter(GameObject buttToActivate)
    {
        if(buttToActivate.activeInHierarchy)
        {
            buttToActivate.SetActive(false);
        }
        else{buttToActivate.SetActive(true);}
    }
    public void LoadHouse(string sceneToMoveTo)
    {
        SceneManager.LoadScene(sceneToMoveTo);
    }
}
