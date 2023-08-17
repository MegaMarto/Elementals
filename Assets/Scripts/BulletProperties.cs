using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour
{
    [SerializeField] float liveTime;
    public float time;


    void Update()
    {
        time += Time.deltaTime;

        if(time > liveTime)
        {
            Destroy(this.gameObject);
        }
    }
}
