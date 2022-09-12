using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetState : TurretState
{
    public override void Update()
    {      
           parent.GhostRotator.LookAt(parent.Target.position + parent.AimOffset);
           parent.Rotator.rotation = Quaternion.RotateTowards(parent.Rotator.rotation, parent.GhostRotator.rotation, Time.deltaTime * parent.RotationSpeed); 
    }


    public override void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            parent.shoot = false;
            parent.Target = null;
            parent.ChangeState(new IdleState());
        }
    }
}
