using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMovement : MonoBehaviour
{


    public GameObject explosion;
    public Transform rangedLocation;
    private float speed = 10;

    void Start()
    {

    }

    

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Dissapear()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
            Instantiate(explosion, rangedLocation.position, rangedLocation.rotation);
        //Dissapear();
    }




}
