using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankAI2 : MonoBehaviour
{
    Transform tr_Player;
    float f_RotSpeed = 1f, f_MoveSpeed = 0.5f;


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

    public GameObject[] PlacementObjectPf;
    GameObject tankPlace;
    string tankName;



    public float offset;

    float timer;
    int waitingTime = 2;

    // Use this for initialization
    void Start()
    {

        int selectedTank = PlayerPrefs.GetInt("selectedTank");
        tankPlace = PlacementObjectPf[selectedTank];
        tankName = tankPlace.name + "(Clone)";
        Debug.Log(tankName);

    }

    // Update is called once per frame
    void Update()
    {
        /* Look at Player*/
        tr_Player = GameObject.Find(tankName).transform;

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

       

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        transform.position = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);



        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation
                                               , Quaternion.LookRotation(tr_Player.position - transform.position)
                                               , f_RotSpeed * Time.deltaTime);

        
        transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;

    }

    private void AttackPlayer()
    {
        

        transform.LookAt(tr_Player);

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
