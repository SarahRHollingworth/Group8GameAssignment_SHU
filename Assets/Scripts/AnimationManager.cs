using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;


public class AnimationManager : MonoBehaviour
{

    public List<AnimatorController> attackAnimations; 
    public List<AnimatorController> walkAnimations;


    void Start()
    {

        //AnimatorController ani = attackAnimations[0];

        //Debug.Log(attackAnimations[0]); //This is for testing my list works.
    

        
    }
    void Update()
    {
        
    }

    public AnimatorController GetAttackAnimatorController(int index)    //Used in PlayerAttack Script to access animation list elements for the attack directions.
    {
        return attackAnimations[index];
    }

    public AnimatorController GetWalkingAnimatorController(int index)   //Used in PlayerBehaviour Script to access animation list elements for the walking directions.
    {
        return walkAnimations[index];
    }
}


//Errors are just Visual Studio being weird I think. Ignore it.