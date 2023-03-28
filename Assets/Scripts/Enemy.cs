using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;

    const float speed = 0.03f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = CalcNextPosition();
    }

    Vector3 CalcNextPosition()
    {
        Vector3 enemyPos = transform.position;

        Vector3 enemyToPlayerDirection = CalcEnemyToPlayerDirection(enemyPos);
        Vector3 unitVectorEP = CalcUnitVector(enemyToPlayerDirection);

        return new Vector3(enemyPos.x + (unitVectorEP.x * speed), enemyPos.y + (unitVectorEP.y * speed), 0.0f);
    }

    Vector3 CalcEnemyToPlayerDirection(Vector3 enemyPos)
    {
        Vector3 playerPos = player.transform.position;

        return new Vector3(playerPos.x - enemyPos.x, playerPos.y - enemyPos.y, 0.0f); //i,j,k 
    }

    Vector3 CalcUnitVector(Vector3 directionVector)
    {
     
        float modulus = Mathf.Sqrt(Mathf.Pow(directionVector.x, 2.0f) + Mathf.Pow(directionVector.y, 2.0f));

        return new Vector3(directionVector.x / modulus, directionVector.y / modulus, 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
