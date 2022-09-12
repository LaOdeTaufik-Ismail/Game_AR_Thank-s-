using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretState
{
    protected Turret parent;
    
    public virtual void Enter(Turret parent)
    {
        this.parent = parent;
    }

    public virtual void Update()
    {
       
    }


    public virtual void Exit(Turret parent)
    {
        this.parent = parent;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
    }

    public virtual void OnTriggerExit(Collider other)
    {   
    }
}
