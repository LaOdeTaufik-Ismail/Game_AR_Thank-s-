using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : TurretState
{

    public override void Update()
    {
        if(parent.DefaultRotation != parent.Rotator.rotation)
        {

            parent.Rotator.rotation = Quaternion.RotateTowards(parent.Rotator.rotation, parent.DefaultRotation, Time.deltaTime * parent.RotationSpeed);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            parent.shoot =true;
            parent.Target = other.transform;
            parent.ChangeState(new FindTargetState());
            
        }
    }
}