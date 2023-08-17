using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public abstract class IceState
{
    protected Rigidbody rigidBody;
    protected GameObject gameObject;
    protected Transform transform;

    public IceState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }

    public abstract Type Tick(); //Method that gets overridden and handles the action in each state;
}
