using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Attributes
    public GameObject player;
    public GameObject manager;

    private PlayerBehaviour playerBehaviour;
    private AnimationManager am;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        am = manager.GetComponent<AnimationManager>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Attack says direction is " + playerBehaviour.GetDirection());
      

        //This should be a switch statement for Case "Right", "Up", "Down", but it just exists as is for proof of concept. Fix this later.


        if (playerBehaviour.GetDirection() == "Right")
        {
        animator.runtimeAnimatorController = am.GetAttackAnimatorController(1);

        }
        else
        {
            animator.runtimeAnimatorController = am.GetAttackAnimatorController(0);
        }

        
    }
}
