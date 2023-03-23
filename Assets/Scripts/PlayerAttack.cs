using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Attributes
    public GameObject player;
    public GameObject manager;
    public GameObject attack;
    private SpriteRenderer sr;

    private PlayerBehaviour playerBehaviour;
    private AnimationManager am;

    private Animator animator;

    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        
        am = manager.GetComponent<AnimationManager>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Attack says direction is " + playerBehaviour.GetDirection());


        //This should be a switch statement for Case "Right", "Up", "Down", but it just exists as is for proof of concept. Fix this later.


       


        if (Input.GetKey(KeyCode.Space))
        {
            sr.enabled = true;
            attacking = true;
            animator.enabled = true;


            Debug.Log("Attackkkkk!!!");


            switch (playerBehaviour.GetDirection())
            {
                case "Right":
                    sr.flipX = false;
                    animator.runtimeAnimatorController = am.GetAttackAnimatorController(1);
                    break;
                case "Left":
                    sr.flipX = true;
                    animator.runtimeAnimatorController = am.GetAttackAnimatorController(1);
                    break;
                case "Up":
                    animator.runtimeAnimatorController = am.GetAttackAnimatorController(0);
                    break;
                case "Down":
                    animator.runtimeAnimatorController = am.GetAttackAnimatorController(2);
                    break;
            }
    
           
    


        }else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.enabled = false;
            attacking = false;
            sr.enabled = false;
        }



    }

    public bool IsAttacking()
    {
        return attacking;

    }


    }

