using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementHori(Input.GetAxis("Horizontal"));
        MovementVert(Input.GetAxis("Vertical"));
    }

    public void MovementVert(float Direction)
    {
        transform.position += transform.up*moveSpeed*Direction*Time.fixedDeltaTime;
    }
    public void MovementHori(float Direction)
    {
        transform.position += transform.right * moveSpeed * Direction* Time.fixedDeltaTime;
    }
}
