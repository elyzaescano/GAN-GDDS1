using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadPuzzle : MonoBehaviour
{
    public ItemSpawn itemSpawn;
    public GameObject keyPadUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        keyPadUI.SetActive(true);
    }

    public void Close()
    {
        keyPadUI.SetActive(false);
        Destroy(GetComponent<KeyPadPuzzle>());
    }
}
