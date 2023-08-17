using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceChaseState : IceState
{
    private Rigidbody rb;
    private LayerMask layerMask;
    [SerializeField] private float speed = 20f;
    private Vector3 direction;
    private IceAI IceMan;
    float attackRange = 10f;
    private Quaternion desiredRotation;
    private float rayDistance = 10f;


    public IceChaseState(IceAI iceMan) : base(iceMan.gameObject)
    {
        IceMan = iceMan;
    }

    public override Type Tick()
    {
        var capturePoint = this.gameObject.GetComponent<IceTarget>().FoundCapPoint();
        if (IceMan.Target == null) { return typeof(IceWanderState); }

        transform.LookAt(IceMan.Target);
        direction = Vector3.Normalize(direction) * 10;
        rb = IceMan.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);

        var distance = Vector3.Distance(transform.position, IceMan.Target.transform.position);
        if(distance <= attackRange)
        {
            return typeof(IceAttackState);
        }

        if (IsForwardBlocked())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 1F);
        }
        else
        {
            Vector3 direction = capturePoint.transform.position - this.transform.position;
            transform.LookAt(capturePoint.transform);
            direction = Vector3.Normalize(direction) * 10;
            rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
            //Debug.Log("ChaseMOVING");
            //Debug.Log(direction);
        }

        Debug.DrawRay(transform.position, direction * rayDistance, Color.red);
        while (IsPathBlocked())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 1F);
        }

        return null;
    }
    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, rayDistance, layerMask);
    }
    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        return Physics.SphereCast(ray, 0.5f, rayDistance, layerMask);
    }
}
