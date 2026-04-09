using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRb;
    GameObject nextTarget;
    int currTarget;
    List<GameObject> targetsList;
    [SerializeField] float movVel;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        currTarget = 0;
    }

    void Start()
    {
        targetsList = new List<GameObject>(PathManager.instance.pathPoint);
        nextTarget = targetsList[currTarget];
        MoveEnemy();
    }
    void Update()
    {
        CheckDistanceToTarget();
    }

    void MoveEnemy()
    {
        float xToMove = nextTarget.transform.position.x - transform.position.x;
        float yToMove = nextTarget.transform.position.y - transform.position.y;
        Vector2 directionToMove = new Vector2(xToMove, yToMove);

        enemyRb.velocity = directionToMove.normalized * movVel;
    }

    void CheckDistanceToTarget()
    {
        float distanceToTarget = Vector2.Distance(transform.position, nextTarget.transform.position);
        
        if(distanceToTarget <= 0.15f)
        {
            currTarget++;
            if(currTarget > targetsList.Count - 1)
            {
                WaveManager.instance.RemoveEnemyFromList(gameObject);
                PlayerInfo.instance.TakeDamage(1);
                Destroy(gameObject);
                return;
            }
            nextTarget = targetsList[currTarget];
            MoveEnemy();
        }
    }
}
