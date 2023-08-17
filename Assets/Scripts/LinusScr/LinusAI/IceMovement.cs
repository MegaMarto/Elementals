using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class IceMovement : MonoBehaviour
{
    public IceTarget attackTarget;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 20;
    private GameObject leader;
    private float attackRange = 8f;
    private float rayDistance = 10f;
    private float stoppingDistance = 1.5f;
    private Vector3 destination;
    private Quaternion desiredRotation;
    private Vector3 direction;
    private GameObject target;
    private IceMoveState currentState;

    private void Start()
    {
        
    } // Update is called once per frame
    void Update()
    {
        direction = Vector3.Normalize(direction) * 10;
        //rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
        switch (currentState)
        {
            case IceMoveState.Search:
                {
                    rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
                    if (NeedDestination())
                    {
                        rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
                        Debug.Log("NewDestination");
                        GetDestination();
                    }

                    var rayColor = IsPathBlocked() ? Color.red : Color.green;
                    Debug.DrawRay(transform.position, direction * rayDistance, rayColor);
                    while (IsPathBlocked())
                    {
                        Debug.Log("Can't walk here");
                        GetDestination();
                    }
                    var targetToAttack = CheckForAttack();
                    if (targetToAttack != null)
                    {
                        target = this.gameObject.GetComponent<IceTarget>().FoundCapPoint();
                        currentState = IceMoveState.Chase;
                    }
                    
                    if(this.gameObject.GetComponent<IceTarget>().FoundCapPoint())
                    {
                        //currentState = IceMoveState.Capture;
                    }
                    transform.rotation = desiredRotation;

                    transform.LookAt(target.transform);
                    Debug.Log("Searching");
                    break;
                }
            case IceMoveState.Chase:
                {
                    Debug.Log("Chasing");
                    if (target == null)
                    {
                        currentState = IceMoveState.Search;
                        return;
                    }
                    transform.LookAt(target.transform);
                    rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
                    if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
                    {
                        currentState = IceMoveState.Attack;
                    }
                    break;
                }
            case IceMoveState.Attack:
                {
                    if(target != null)
                    {
                       //
                    }
                    currentState = IceMoveState.Search;
                    break;
                }
            case IceMoveState.Capture:
                {
                    if (target != null)
                    {
                        transform.LookAt(target.transform);
                        rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
                        Debug.Log("Capture");
                    }
                    currentState = IceMoveState.Search;
                    break;
                }
                
              
        }
    }
    private bool IsPathBlocked()
    {
        Ray ray = new Ray(this.transform.position, this.direction);
        RaycastHit hit;
        var hitSomething = Physics.RaycastAll(ray, rayDistance, layerMask);
        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {
            Debug.Log("Hit");
        }
        //rb.AddForce(this.gameObject.transform.right * speed * Time.deltaTime, ForceMode.Impulse);
        //Debug.Log("Hit");
        return hitSomething.Any();
        

    }
    private bool NeedDestination()
    {
        if (destination == Vector3.zero) { return true; }

        var distance = Vector3.Distance(transform.position, destination);
        if (distance <- stoppingDistance)
        {
            return true;
        }
        return false;
    }
    
    private void GetDestination()
    {
        //Vector3 movePos = (transform.position + (transform.forward * 4f)) + 
        //                  new Vector3(Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));
        target = this.gameObject.GetComponent<IceTarget>().FoundCapPoint();


    }

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
    private Transform CheckForAttack()
    {
        //Ray ray = new Ray(this.transform.position, this.direction);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        //{
        //    Debug.Log(hit.transform.gameObject);
        //}
        float rayRange = 10f;

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 25; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, rayRange))
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
                Debug.DrawRay(pos, direction * rayRange, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;

    }
    public enum IceMoveState
    {
        Search,
        Attack,
        Capture,
        Chase
    }
}
