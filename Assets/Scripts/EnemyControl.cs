using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyControl : MonoBehaviour
{
    private NavMeshAgent agent;
    public enum EnemyState { IDLE, PATROL, FOLLOW, ALERT, ATTACK, DAMAGE, DEATH }
    public EnemyState state;
    public GameObject[] WayPoints;
    private int currentPoint;
    public Animator anim;
    private GameObject currentTarget;
    private RaycastHit hitEyes;
    void Start()
    {
        currentPoint = 0;
        agent = GetComponent<NavMeshAgent>();

    }
    void Update()
    {
        switch (state)
        {
            case EnemyState.IDLE:

                break;
            case EnemyState.PATROL:
                anim.SetBool("Move", true);
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(WayPoints[currentPoint].transform.position);
                    currentPoint++;
                    if (currentPoint >= WayPoints.Length)
                        currentPoint = 0;
                }
                break;
            case EnemyState.FOLLOW:
                agent.SetDestination(currentTarget.transform.position);
                break;
            case EnemyState.ALERT:
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    state = EnemyState.PATROL;
                }
                break;
            case EnemyState.ATTACK:

                break;
            case EnemyState.DAMAGE:

                break;
            case EnemyState.DEATH:

                break;
        }
        if (currentTarget != null)
        {
            if (Physics.Linecast(transform.position, currentTarget.transform.position, out hitEyes))
            {
                if (hitEyes.collider.tag == "Player")
                {
                    state = EnemyState.FOLLOW;
                    Debug.DrawLine(transform.position, hitEyes.point, Color.red);
                }
                else
                    Debug.DrawLine(transform.position, hitEyes.point);
            }
        }
    }
    public void OnTriggerEnterChild(Collider other)
    {
        currentTarget = other.gameObject;
    }
    public void OnTriggerExitChild(Collider other)
    {
        if (state == EnemyState.FOLLOW)
        {
            state = EnemyState.ALERT;
            agent.SetDestination(currentTarget.transform.position);
        }
        currentTarget = null;
    }
}
