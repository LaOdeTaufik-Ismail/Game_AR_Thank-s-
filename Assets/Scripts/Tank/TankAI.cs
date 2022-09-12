
using UnityEngine;
using UnityEngine.AI;

public class TankAI : MonoBehaviour
{
    public NavMeshAgent tank;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;


    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public float m_LaunchForce = 3f;

    public NavMeshSurface surface;

    public float offset;

    float timer;
    int waitingTime = 2;


    private void Awake()
    {

        player = GameObject.Find("Tank AR(Clone)").transform;
        tank = GetComponent<NavMeshAgent>();
        surface.BuildNavMesh();
    }

    private void Update()
    {
        
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            Debug.Log("Patrol");

        }
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            tank.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);



        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        tank.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        tank.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                Rigidbody shellInstance =
                Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

                shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
                timer = 0;

            }
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}