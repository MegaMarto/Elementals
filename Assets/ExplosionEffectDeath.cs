using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectDeath : MonoBehaviour
{
    public GameObject explosion;
    public Transform bulletLocation;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, bulletLocation.position, bulletLocation.rotation);
        //Dissapear();
    }
}
