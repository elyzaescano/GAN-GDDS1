using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    public float moveSpeed = 5;
    private float turnRate = 5f;
    Rigidbody2D rb;
    Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      //movement.x = Input.GetAxis("Horizontal");
      //movement.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Move(float dirX, float dirY)
    {
        movement.x = dirX;
        movement.y = dirY;
    }
    public void Turn(float dir)
    {
        rb.rotation += turnRate * dir * Time.deltaTime;
    }


}
