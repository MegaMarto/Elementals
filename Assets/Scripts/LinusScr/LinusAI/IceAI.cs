using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class IceAI : MonoBehaviour
{
    public Transform Target { get; private set; }
    public Transform CapPoint { get; private set; }
    public IceStateMachine iceStateMachine => GetComponent<IceStateMachine>();

    private void Awake()
    {
        InitializeStateMachine();
    }
    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, IceState>
        {
            {typeof(IceWanderState), new IceWanderState(this) },
            {typeof(IceChaseState), new IceChaseState(this) },
            {typeof(IceCaptureState), new IceCaptureState(this) },
            {typeof(IceAttackState), new IceAttackState(this) }
        };

        GetComponent<IceStateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
    public void SetCapPoint(Transform capPoint)
    {
        CapPoint = capPoint;
    }
}