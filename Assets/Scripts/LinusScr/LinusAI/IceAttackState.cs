using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class IceAttackState : IceState
{
    private Rigidbody rb;
    private LayerMask layerMask;
    [SerializeField] private float speed = 2f;
    private Vector3 direction;
    private IceAI IceMan;
    float attackRange = 10f;
    private Quaternion desiredRotation;
    private float rayDistance = 10f;
    private PlayerAttacks attack;

    public IceAttackState(IceAI iceMan) : base(iceMan.gameObject)
    {
        IceMan = iceMan;
    }
 

    public override Type Tick()
    {
        if (IceMan.Target == null) { return typeof(IceWanderState); }
        transform.LookAt(IceMan.Target);
        direction = Vector3.Normalize(direction) * 10;
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
        if (IceMan.Target != null)
        {
            attack.LaunchBullet();
        }

        return null;
    }
}
