using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{

    NavMeshAgent enemyAgent;
    Animator enemyAnimator;
    Rigidbody EnemyRigidbody;


    [SerializeField] Transform Target;
    [SerializeField] float lookRadius, reactionRadius, attackRadius;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            enemyAgent.speed = 0;
        }
    }

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyAgent = GetComponent<NavMeshAgent>();
        EnemyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromTarget = Vector3.Distance(Target.position, transform.position);

        if(distanceFromTarget <= lookRadius)
        {
            enemyAgent.isStopped = false;
            enemyAgent.SetDestination(Target.position);
        }

        if (enemyAgent.remainingDistance > lookRadius)
        {
            enemyAgent.speed = 0f;
            enemyAnimator.SetBool("Walk Forward", false);
        }
        else
        {
            enemyAgent.speed = 3f;
            enemyAnimator.SetBool("Walk Forward", true);
            enemyAgent.SetDestination(Target.position);
        }

        if(distanceFromTarget <= 3)
        {
            enemyAnimator.SetBool("isAttacking", true);
        }
        else
        {
            enemyAnimator.SetBool("isAttacking", false);
        }

    }
}
