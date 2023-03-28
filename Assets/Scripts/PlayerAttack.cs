using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Attributes
    public GameObject player;
    public GameObject manager;
    public GameObject spikeTrap;
    private SpriteRenderer sr;

    private PlayerBehaviour playerBehaviour;
    private AnimationManager am;

    private Animator animator;

    private Vector3 playerBottomCentreOffset;

    float attackTimer;
    const float maxAttackTimer = 0.2f;
    bool attacking;

    float basicAttackCooldown;
    const float maxBasicAttackCooldown = 0.3f;
    bool canAttack;

    float spikeTrapCooldown;
    const float maxSpikeTrapCooldown = 10.0f;
    bool canAttackSpikeTrap;

    float speedUpTimer;
    const float maxSpeedUpTimer = 5.0f;
    bool speedUp;

    float speedUpCooldown;
    const float maxSpeedUpCooldown = 5.0f;
    bool canAttackSpeedUp;


    float healMeCooldown;
    const float maxHealMeCooldown = 25.0f;
    bool canAttackHealMe;


    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();

        am = manager.GetComponent<AnimationManager>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        float playerYSize = player.GetComponent<SpriteRenderer>().bounds.size.y;
        float spikeTrapYSize = spikeTrap.GetComponent<SpriteRenderer>().bounds.size.y;

        playerBottomCentreOffset = new Vector3(0.0f, (spikeTrapYSize - playerYSize) * 0.5f, 0.0f);

        basicAttackCooldown = maxBasicAttackCooldown;
        canAttack = true;

        attackTimer = maxAttackTimer;
        attacking = false;

        spikeTrapCooldown = 0.0f;
        canAttackSpikeTrap = true;

        speedUpTimer = maxSpeedUpTimer;
        speedUp = false;

        speedUpCooldown = 0.0f;
        canAttackSpeedUp = true;


        healMeCooldown = 0.0f;
        canAttackHealMe = true;

    }

    // Update is called once per frame
    void Update()
    {

        AttackFunc();



    }



    void AttackFunc()
    {

        Debug.Log("Attack says direction is " + playerBehaviour.GetDirection());


        //This should be a switch statement for Case "Right", "Up", "Down", but it just exists as is for proof of concept. Fix this later.
        bool basicAtackInput = GetPlayerBasicAttackInput();
        UseBasicAttack(basicAtackInput);
        string skillUsed = InputSkillAttack();
        UseSkill(ref skillUsed);

    }




    bool GetPlayerBasicAttackInput()
    {
        if (!canAttack)
        {
            DecrementBasicAttackCooldown();
            return false;
        }

        if (attacking || Input.GetKeyDown(KeyCode.Space))
        {
            attacking = true;
            return true;

        }
        else
        {
            return false;
        }


    }




    void DecrementBasicAttackCooldown()
    {
        basicAttackCooldown -= Time.deltaTime;

        if (basicAttackCooldown < 0)
        {
            canAttack = true;
            basicAttackCooldown = maxBasicAttackCooldown;
        }
    }

    void UseBasicAttack(bool basicAtackInput)
    {


        if (basicAtackInput)    //Note in playerBehaviour class, player velocity is set to 1/2 when player uses their basic attack
        {

            sr.enabled = true;
            animator.enabled = true;


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


            DecrementAttackTimer();


        }
    }

    void DecrementAttackTimer()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer < 0)
        {
            StopAttacking();
        }
    }

    void StopAttacking()
    {
        animator.enabled = false;
        attacking = false;
        sr.enabled = false;
        canAttack = false;
        attackTimer = maxAttackTimer;
    }


    string InputSkillAttack()
    {
        if (Input.GetKey(KeyCode.E))
        {
            return "spikeTrap";


        } else if (Input.GetKey(KeyCode.F))
        {
            return "speedup";
        } else if (Input.GetKey(KeyCode.Q))
        {
            return "healme";
        }
        else return "";

    }

    void UseSkill(ref string skillUsed) {

        if (skillUsed == "spikeTrap" && canAttackSpikeTrap)
        {
            spikeTrapCooldown = maxSpikeTrapCooldown;
            canAttackSpikeTrap = false;
            LaySpikeTrap();

        } else if (skillUsed == "speedup" && canAttackSpeedUp)
        {
            speedUpCooldown = maxSpeedUpCooldown;
            speedUp = true;
            canAttackSpeedUp = false;

        } else if (skillUsed == "healme" && canAttackHealMe)
        {
            healMeCooldown = maxHealMeCooldown;
            canAttackHealMe = false;
            Healme();
        }

        DecrementSkillCooldown();
        DecrementSkillTimer();

    }

    void DecrementSkillCooldown()
    {

        if (!canAttackSpikeTrap)
        {
            spikeTrapCooldown -= Time.deltaTime;

            if (spikeTrapCooldown < 0)
            {
                canAttackSpikeTrap = true;
                spikeTrapCooldown = 0.0f;
            }
        }

        if (!canAttackSpeedUp && !speedUp)
        {
            speedUpCooldown -= Time.deltaTime;

            if (speedUpCooldown < 0)
            {
                canAttackSpeedUp = true;
                speedUpCooldown = 0.0f;
            }
        }

        if (!canAttackHealMe)
        {
            healMeCooldown -= Time.deltaTime;

            if (healMeCooldown < 0)
            {
                canAttackHealMe = true;
                healMeCooldown = 0.0f;
            }
        }
    }

    void DecrementSkillTimer()
    {
        if (speedUp)
        {
            speedUpTimer -= Time.deltaTime;

            if (speedUpTimer < 0)
            {
                speedUp = false;
                speedUpTimer = maxSpeedUpTimer;
            }
        }
    }

    void LaySpikeTrap()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 spikeTrapSpawnPoint = playerPos + playerBottomCentreOffset;
        Instantiate(spikeTrap, spikeTrapSpawnPoint, player.transform.rotation);

    }

    void Healme()
    {
        int amount = 1;
        playerBehaviour.IncrementHealth(amount);


    }


    public bool IsAttacking() //Halves Speed while using basic attack for balancing purposes.
    {
        return attacking;

    }

    public bool UseSpeedUp()
    {
        return speedUp;
    }

    public float GetCooldownSpikeTrap(){

        return spikeTrapCooldown;

    }

    public float GetCooldownSpeedUp()
    {

        return speedUpCooldown;

    }

    public float GetCooldownHealMe()
    {

        return healMeCooldown;

    }

    public float GetMaxCooldownSpikeTrap()
    {
        return maxSpikeTrapCooldown;
    }

    public float GetMaxCooldownSpeedUp()
    {
        return maxSpeedUpCooldown;
    }

    public float GetMaxCooldownHealMe()
    {
        return maxHealMeCooldown;
    }

}


