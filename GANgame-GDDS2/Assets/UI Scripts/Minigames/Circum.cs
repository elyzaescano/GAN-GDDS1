using UnityEngine;


public class Circum : MonoBehaviour 
{
    GameObject player;
    private void Start() 
    {
        player = GameObject.FindWithTag("Player");
    }
    public void Circumventating(Transform destination)
    { 
        player.transform.position = destination.position;
    }
}