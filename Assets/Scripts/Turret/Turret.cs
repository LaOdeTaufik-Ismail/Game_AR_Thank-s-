using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    protected TurretState currentState;

    public Transform Target { get; set; }

    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public float m_LaunchForce = 20f;
    float timer;
    int waitingTime = 2;
    public bool shoot = false;

    

    [SerializeField]
    private Vector3 aimOffset; 

    [SerializeField]
    private Transform rotator;

    [SerializeField]
    private Transform ghostRotator;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private GameObject shell;

    public Quaternion DefaultRotation { get; set; }

    public Transform Rotator { get => rotator; set => rotator = value; }

    public Vector3 AimOffset { get => aimOffset; set => aimOffset = value; }

    public Transform GhostRotator { get => ghostRotator; set => ghostRotator = value; }

    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    

    private void Start()
    {

        DefaultRotation = rotator.rotation;
        ChangeState(new IdleState());
    }


    private void Update()
    {
        if(GhostRotator != null)
        {
            currentState.Update();
            if (shoot)
            {
                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    Shoot();
                    timer = 0;

                }
            }

        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Shoot()
    {
        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward; 
    }


    public void ChangeState(TurretState newState)
    {
        if(newState != null)
        {
            newState.Exit(this);
        }
        this.currentState = newState;
        newState.Enter(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

   
}
