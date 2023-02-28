using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Player;
    public float Distance;

    public bool isAngered;
    public bool isWalking;
    Animator EnemyAnimator;

    public UnityEngine.AI.NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if(Distance <=5)
        {
            isAngered = true;
            EnemyAnimator.SetBool("isWalking", true);
        }
        if(Distance > 5f)
        {
            isAngered = false;
            EnemyAnimator.SetBool("isWalking", false);
        }
        if(isAngered)
        {
            _agent.isStopped = false;

            _agent.SetDestination(Player.transform.position);
        }
        if(!isAngered)
        {
            _agent.isStopped = true;
        }
        if (Distance <=1)
        {
            EnemyAnimator.SetBool("isAttacking", true);
        }
        if (Distance > 1)
        {
            EnemyAnimator.SetBool("isAttacking", false);
        }
    }
}
