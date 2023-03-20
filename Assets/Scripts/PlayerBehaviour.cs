using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Attributes
    private float speed;
    private bool inWall;
    private int wallQuadrant = 0;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Sprite[] spriteArray = new Sprite[3];
 

    void Start()
    {
        speed = 4f;
        inWall = false;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //TODO: Change to gate location
        sr.sprite = spriteArray[0];
    }

    void FixedUpdate()
    {
        PlayerMovementFunc();
    }

    void PlayerMovementFunc()
    {
        Vector2 directionalInput = GetDirectionalInput();
        Vector2 velocity = CalcVelocity(directionalInput);
        SetPlayerOrientation(directionalInput, velocity);

        if (inWall)
        {
            RestrictVelocity(velocity);
        }

        SetVelocity(velocity);
    }

    Vector2 GetDirectionalInput()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        //Direction =
        return new Vector2(inputHorizontal, inputVertical);
    }

    Vector2 CalcVelocity(Vector2 direction) 
    { 
        return new Vector2(direction.x * speed, direction.y * speed);
    }

    void SetPlayerOrientation(Vector2 direction, Vector2 velocity)
    {
        // if we are receving vertical directional input
        if (direction.y != 0)
        {
            if (velocity.y > 0)
            {
                sr.sprite = spriteArray[1];
            }
            else
            {
                sr.sprite = spriteArray[2];
            }
        }
        else if (direction.x != 0)
        {
            sr.sprite = spriteArray[0];
        }
        
        // if we are receving horizontal directional input
        if (direction.x != 0)
        {
            //sr.flipX = velocity.x < 0;
            if (velocity.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }

    void RestrictVelocity(Vector2 velocity)
    {
        switch (wallQuadrant)
        {
            // Top-left quadrant
            case 0:
                if (velocity.x < 0)
                    velocity.x = 0;
                if (velocity.y > 0)
                    velocity.y = 0;
                break;
            // Top-right quadrant
            case 1:
                if (velocity.x > 0)
                    velocity.x = 0;
                if (velocity.y > 0)
                    velocity.y = 0;
                break;
            // Bottom-left quadrant
            case 2:
                if (velocity.x < 0)
                    velocity.x = 0;
                if (velocity.y < 0)
                    velocity.y = 0;
                break;
            // Bottom-right quadrant
            case 3:
                if (velocity.x > 0)
                    velocity.x = 0;
                if (velocity.y < 0)
                    velocity.y = 0;
                break;
        }
    }

    void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            CalcWallQuadrant();
            inWall = true;
        }
    }

    // Bug fix incase player gets stuck in wall
    //private void OnCollisionStay2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Wall")
    //    {
    //        CalcWallQuadrant();
    //    }
    //}

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            inWall = false;
        }
    }
    
    void CalcWallQuadrant()
    {
        Vector3 playerPos = transform.position;

        if (playerPos.x < 0)
        {
            // bottom-left quadrant
            if (playerPos.y < 0)
            {
                wallQuadrant = 2;
            }
            // top-left quadrant
            else 
            {
                wallQuadrant = 0;
            }
        }
        else
        {
            // bottom-right quadrant
            if (playerPos.y < 0)
            {
                wallQuadrant = 3;
            }
            // top-right quadrant
            else
            {
                wallQuadrant = 1;
            }
        }
    }
}
