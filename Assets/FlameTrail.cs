using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrail : MonoBehaviour
{
    public GameObject debree;
    public Transform playerLocation;


    public float cooldown;
    float lastShot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastShot < cooldown)
        {
            return;
        }
        lastShot = Time.time;
        Instantiate(debree, playerLocation.position, playerLocation.rotation);
    }
}
