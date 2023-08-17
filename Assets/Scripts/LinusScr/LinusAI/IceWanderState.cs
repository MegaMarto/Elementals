using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class IceWanderState : IceState
{
    public IceTarget attackTarget;
    private LayerMask layerMask;
    [SerializeField] private float speed = 2f;
    private float turnSpeed = 1f;
    private float attackRange = 8f;
    private float rayDistance = 10f;
    private float stoppingDistance = 1.5f;
    Vector3 destination;
    private Quaternion desiredRotation;
    private Vector3 direction = Vector3.forward;
    private IceAI IceMan;

    public IceWanderState(IceAI iceMan) : base(iceMan.gameObject)
    {
        IceMan = iceMan;
    }
    public override Type Tick()
    {
        var target = this.gameObject.GetComponent<IceTarget>().FoundCapPoint();
        var rb = IceMan.gameObject.GetComponent<Rigidbody>();
        var capturePoint = CaptureThePoint();
        var chaseTarget = CheckForAttack();
        if (capturePoint != null)
        {
            IceMan.SetCapPoint(capturePoint);
            return typeof(IceCaptureState);
        }
        if (chaseTarget != null)
        {
            IceMan.SetTarget(chaseTarget);
            return typeof(IceChaseState);
        }
        if (destination != null || Vector3.Distance(transform.position, destination) <= stoppingDistance)
        {
            //Find New destination
        }
        
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);


        if (IsForwardBlocked())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 10F);
        }
        else
        {
            Vector3 direction = target.transform.position - this.transform.position;
            transform.LookAt(target.transform);
            direction = Vector3.Normalize(direction) * 10;
            rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
            //Debug.Log("MOVING");
            //Debug.Log(direction);
        }

        Debug.DrawRay(transform.position, direction * rayDistance, Color.red);
        while (IsPathBlocked())
        {
            rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 10F);
        }

        return null;
    }
    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.Log("ForwardBlock");
        return Physics.SphereCast(ray, 1f, rayDistance, layerMask);
    }
    private bool IsPathBlocked()
    {

        Ray ray = new Ray(transform.position, direction);
        //Debug.Log("PathBlock");
        return Physics.SphereCast(ray, 1f, rayDistance, layerMask);
        
    }

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAttack()
    {
        float attackRange = 10f;

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 25; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, attackRange))
            {
                var IceAI = hit.collider.GetComponent<IceMovement>();
                if (IceAI != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return IceAI.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * attackRange, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;

    }
    private Transform CaptureThePoint()
    {
        return null;
    }
}