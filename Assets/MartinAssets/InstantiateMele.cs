using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMele : MonoBehaviour
{
    public Rigidbody meleObject;
    public Transform MeleSource;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(meleObject, MeleSource.position, MeleSource.rotation);
        }
    }
}
