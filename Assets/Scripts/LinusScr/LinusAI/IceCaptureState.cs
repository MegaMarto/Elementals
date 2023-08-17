using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class IceCaptureState : IceState
{
    private Rigidbody rb;
    private LayerMask layerMask;
    [SerializeField] private float speed = 2f;
    private Vector3 direction;
    private IceAI IceMan;
    float attackRange = 10f;
    private Quaternion desiredRotation;
    private float rayDistance = 10f;


    public IceCaptureState(IceAI iceMan) : base(iceMan.gameObject)
    {
        IceMan = iceMan;
    }
    

    public override Type Tick()
    {

        return null;
    }
}
