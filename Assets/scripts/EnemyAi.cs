using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    //needed ai stuff
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask grounded;
    public LayerMask playerlayer;


    //locating player
    public Vector3 walkpoint;
    private bool isWalkpointSet;
    public float walkingRange;

    //attack
    public float yoffset = 4.7f;
    public GameObject projectile;
    public float attackTime;
    private bool justAttacked;
    public float damage = 1f;
    public float health = 50f;
    Vector3 updatePos;
    Vector3 moveDir;
    //State machines
    public float visionRange;
    public float attackRange;
    public bool isPlayerSeen;
    public bool isPlayerAttackable;

    private void Awake()
    {
        player = GameObject.Find("player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        projectile.transform.LookAt(player);
        moveDir = (player.position - transform.position).normalized * (attackRange+1);
        updatePos = transform.position;
        updatePos.y = transform.position.y+ yoffset;
        Debug.DrawRay(transform.position, transform.forward*attackRange,Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            playHP target = hit.transform.GetComponent<playHP>();
            if (target != null)
            {
               
            }
        }
        //stops movement
        isPlayerSeen = Physics.CheckSphere(transform.position, visionRange, playerlayer);
        isPlayerAttackable = Physics.CheckSphere(transform.position, attackRange, playerlayer);

        if(!isPlayerAttackable && !isPlayerSeen)
        {
            findingPlayer();
        }
        if (!isPlayerAttackable && isPlayerSeen)
        {
            chasePlayer();
        }
        if (isPlayerAttackable && isPlayerSeen)
        {
            attacking();
        }
    }
    void searchPoint()
    {
        float randZ = Random.Range(-walkingRange, walkingRange);
        float randX = Random.Range(-walkingRange, walkingRange);

        walkpoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);
        if (Physics.Raycast(walkpoint, -transform.up, 2f, grounded))
        {
            isWalkpointSet = true;
        }
    }
    void findingPlayer()
    {
        if (!isWalkpointSet)
        {
            searchPoint();
        }
        if (isWalkpointSet)
        {
            agent.SetDestination(walkpoint);
        }
        Vector3 distToWalkPoint = transform.position - walkpoint;

        if (distToWalkPoint.magnitude < 1f)
        {
            isWalkpointSet = false;
        }
    }

    void chasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void resetAttack()
    {
        justAttacked = false;
    }
    //dying
    void die()
    {
        Destroy(gameObject);
    }
    //taking damage


    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            die();
        }
    }

    void attacking()
    {        
 
        agent.SetDestination(transform.position);
        //faces enemy toward player


        if (!justAttacked)
        {
            Rigidbody rb = Instantiate(projectile, updatePos, transform.rotation).GetComponent<Rigidbody>();
            
            rb.velocity = moveDir;

            justAttacked = true;
            Invoke(nameof(resetAttack), attackTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward);
    }

}
