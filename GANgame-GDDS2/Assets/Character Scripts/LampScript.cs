using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LampScript : ItemSpawn
{
    public float timeToBurn;
    float currentBurnTime;
    bool isBurning = false;
    public UnityEvent LampBurnEvent;

    public Light candleLight;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isBurning) {
            currentBurnTime -= Time.deltaTime;
            candleLight.intensity -= Time.deltaTime / (timeToBurn/2);
                }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerInv = collision.GetComponent<InventoryObject>();
        if(playerInv.equippedItem == itemRequired)
        {
            EventManager.InteractEvent += startTheBurn;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EventManager.InteractEvent -= startTheBurn;
    }

    public void startTheBurn()
    {
        isBurning = true;
        Invoke("SpawnAfterBurn", timeToBurn);
        LampBurnEvent?.Invoke();
        currentBurnTime = timeToBurn;
        candleLight.intensity = 4f;
    }

    public void SpawnAfterBurn()
    {
        itemNeeded = true;
        canSpawn = true;
        Spawn();
        isBurning = false;
    }

}
