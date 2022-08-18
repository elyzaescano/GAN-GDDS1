using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTransition : MonoBehaviour 
{
        public void ChangeScene(string sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
}