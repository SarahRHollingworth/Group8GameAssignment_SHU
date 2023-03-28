using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject attack;
    public GameObject player;
    public GameObject[] cooldownUI = new GameObject[3];
    public GameObject[] healthpoint = new GameObject[6];

    private PlayerAttack atk;
    private PlayerBehaviour playerBehaviour;
    private float[] attackMaxCooldowns = new float[3];

    private SpriteRenderer[] srHealth = new SpriteRenderer[6];
    
    // Start is called before the first frame update
    void Start()
    {
        atk = attack.GetComponent<PlayerAttack>();
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        
        attackMaxCooldowns[0] = atk.GetMaxCooldownSpikeTrap();
        attackMaxCooldowns[1] = atk.GetMaxCooldownSpeedUp();
        attackMaxCooldowns[2] = atk.GetMaxCooldownHealMe();

        for(int i = 0; i < 6; ++i)
        {
            srHealth[i] = healthpoint[i].GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCooldownMeters();
        UpdateHealth();
        
    }

    void UpdateCooldownMeters()
    {
        float currentSpikeCooldown = atk.GetCooldownSpikeTrap();
        if (currentSpikeCooldown < 0)
            currentSpikeCooldown = 0;
        float yScale = atk.GetCooldownSpikeTrap() / attackMaxCooldowns[0];
        cooldownUI[0].transform.localScale = new Vector3(1.0f, yScale, 1.0f);

        float currentSpeedUpCooldown = atk.GetCooldownSpeedUp();
        if (currentSpeedUpCooldown < 0)
            currentSpeedUpCooldown = 0;
        yScale = atk.GetCooldownSpeedUp() / attackMaxCooldowns[1];
        cooldownUI[1].transform.localScale = new Vector3(1.0f, yScale, 1.0f);

        float currentHealMeCooldown = atk.GetCooldownHealMe();
        if (currentHealMeCooldown < 0)
            currentHealMeCooldown = 0;
        yScale = atk.GetCooldownHealMe() / attackMaxCooldowns[2];
        cooldownUI[2].transform.localScale = new Vector3(1.0f, yScale, 1.0f);

    }

    void UpdateHealth()
    {

        int currentHealth = playerBehaviour.GetPlayerHealth();


        for(int i = 0; i < 6; i++)
        {
            if (i < currentHealth)
                srHealth[i].enabled = true;
            else
                srHealth[i].enabled = false;

        }

    }
    
    


        
}
