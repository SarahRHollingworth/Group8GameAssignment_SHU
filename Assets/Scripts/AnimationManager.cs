using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AnimationManager : MonoBehaviour
{

    public List<RuntimeAnimatorController> attackAnimations; 
    public List<RuntimeAnimatorController> walkAnimations;


    void Start()
    {

        //AnimatorController ani = attackAnimations[0];

        //Debug.Log(attackAnimations[0]); //This is for testing my list works.
    

        
    }
    void Update()
    {
        
    }

    public RuntimeAnimatorController GetAttackAnimatorController(int index)    //Used in PlayerAttack Script to access animation list elements for the attack directions.
    {
        return attackAnimations[index];
    }

    public RuntimeAnimatorController GetWalkingAnimatorController(int index)   //Used in PlayerBehaviour Script to access animation list elements for the walking directions.
    {
        return walkAnimations[index];
    }
}

