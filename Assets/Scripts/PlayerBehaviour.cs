using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Attributes
    private float speed;
    public GameObject manager;
    public GameObject attack;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AnimationManager am;
    private PlayerAttack atk;

    private Animator animator;

    public Sprite[] spriteArray = new Sprite[3];   //For default sprite. Needs updating later for idling in different directions.

    string CharacterIsFacingDirection = "";


    void Start()
    {
        speed = 4f;
        rb = GetComponent<Rigidbody2D>(); //Rigid Body for Player
        sr = GetComponent<SpriteRenderer>();    //Sprite Renderer for Player. Use later.
        atk = attack.GetComponent<PlayerAttack>();
        am = manager.GetComponent<AnimationManager>();  //Animation Manager Class.


        sr.sprite = spriteArray[0]; //Needs updating for idle sprites.

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        PlayerMovementFunc(); //Should assign the character's direction to CharacterIsFacing Direction variable - "Up", "Down", "LeftOrRight". Used in PlayerAttack Script to determine orienation for the attack animation.

    }

    void PlayerMovementFunc()
    {


        Vector2 directionalInput = GetDirectionalInput();
        Vector2 velocity = CalcVelocity(directionalInput);
        SetPlayerOrientation(directionalInput, velocity);
        SetVelocity(velocity);

        
        
    }

    Vector2 GetDirectionalInput()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal"); //Inputs are A and D.
        float inputVertical = Input.GetAxisRaw("Vertical");     //Inputs are W and S.

        //Direction =
        return new Vector2(inputHorizontal, inputVertical);
    }

     Vector2 CalcVelocity(Vector2 direction) 
    {

        if (atk.IsAttacking())
        {
            return new Vector2((direction.x * speed) / 2, (direction.y * speed) / 2);
        }
        return new Vector2(direction.x * speed, direction.y * speed);   //Moves sprite in accordance to player input in GetDirecitonalInput();

    }

    void SetPlayerOrientation(Vector2 direction, Vector2 velocity)
    {
        // if we are receving vertical directional input



        if (direction.y != 0)
        {
            if (velocity.y > 0)
            {

                animator.runtimeAnimatorController = am.GetWalkingAnimatorController(1);        //Access AnimationController in AnimationManager script to know which animation in the list to play according to index. 

                CharacterIsFacingDirection = "Up";  //Updates player orientation as a string for comparison in player Attack function. If CharacterIsFacingDirection = "Up" then the attack animation for Up will be accessed, for example.
                Debug.Log(CharacterIsFacingDirection); //For testing, console should output current direction.

            }
            else
            {
                animator.runtimeAnimatorController = am.GetWalkingAnimatorController(2);

                CharacterIsFacingDirection = "Down";
                Debug.Log(CharacterIsFacingDirection);


            }
        }
        else if (direction.x != 0)
        {

            animator.runtimeAnimatorController = am.GetWalkingAnimatorController(0);
            // sr.sprite = spriteArray[0];


            if (direction.x < 0)
            {
                CharacterIsFacingDirection = "Left";

            }
            else
            {
                CharacterIsFacingDirection = "Right";
            }

            Debug.Log(CharacterIsFacingDirection);


        }

        //Flips Horizontal Sprite Left or Right depending on user input for A or D.
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


        if (direction.y == 0 && direction.x == 0)
        {

            PlayerIdlingOrientation();
        }

       
    }

    void PlayerIdlingOrientation()
    {

        switch (CharacterIsFacingDirection)
        {
            case "Up":
                animator.runtimeAnimatorController = null;
                sr.sprite = spriteArray[1];
                break;
            case "Down":
                animator.runtimeAnimatorController = null;
                sr.sprite = spriteArray[2];
                break;
            case "Right":
            case "Left":
                animator.runtimeAnimatorController = null;
                sr.sprite = spriteArray[0];
                break;
            default:
                Debug.Log("This condition should never be met, but it's here for bug testing");
                sr.sprite = spriteArray[0];
                break;
        }
    

      
    }



    void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }




    public string GetDirection()
    {
        return CharacterIsFacingDirection; //Public function, Returns the retrieved String to use in PlayerAttack class.
    }
}

