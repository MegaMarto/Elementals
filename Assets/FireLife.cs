using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLife : MonoBehaviour
{
    public float health = 100;
    

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ice")
            health -= 20;
        else if (col.gameObject.tag == "Death")
            health -= 40;
        else if (col.gameObject.tag == "Earth")
            health -= 20;
        else if (col.gameObject.tag == "Wind")
            health -= 40;
        if (health <= 0)
            Dead();
    }

    void Update()
    {

    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
