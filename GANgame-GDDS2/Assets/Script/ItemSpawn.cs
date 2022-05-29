using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{

    public GameObject itemPrefab;
    public Transform spawnPoint;
    bool canSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canSpawn = true;
        }
    }

    public void Spawn()
    {
        if (canSpawn)
        {
            Instantiate(itemPrefab, spawnPoint);
            canSpawn = false;
            Destroy(GetComponent<ItemSpawn>());
        }
    }
}
