using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJoystickController : MonoBehaviour
{
    
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       

        float h = VirtualJoystick.GetAxis("Horizontal");
        float v = VirtualJoystick.GetAxis("Vertical");

        Vector2 dir = new Vector2(h, v);
        float a = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        if(dir.sqrMagnitude > 0)
        {
            player.Move(h,v);
           
        }


    }

}


