using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAI : MonoBehaviour
{
    public DeathTarget deathTarget;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;


    public GameObject magicCircle;
    public Transform npcLocation;

    private Vector3 direction;
    //private Vector3 targetPos;
    //private Vector3 localTarget;
    //private float angle;
    //private int velocity = 100;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float bulletForce;
    [SerializeField] private float wallRange;
    public float cooldownTime = 2;
    private float lastAttackTime;
    private float lastWallTime;

    // Start is called before the first frame update
    void Start()
    {
        lastAttackTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(deathTarget.target != null)
        {
            Rotate();
            Movement();
            if(deathTarget.attacking)
            {
                
                Attack();
            }
        }
    }

    void Movement()
    {
        direction = transform.forward;
        direction = Vector3.Normalize(direction) * 10;
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
    }

    void Rotate()
    {
        //  makes rigibody rotate towards the location of the target
        transform.LookAt(deathTarget.target.transform.position);

        //  Too complicated dum dum
        //targetPos = target.transform.position - transform.position;
        //localTarget = transform.InverseTransformPoint(target.transform.position);

        //angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        //Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
        //Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        //rb.MoveRotation(rb.rotation * deltaRotation);

        //  Wihtout the use of rigibody didnt work oop.
        //Vector3 dir = targetPos.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(dir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime > cooldownTime)
        {
            Debug.Log("Cooldown off");
            Vector3 spawnPos = transform.position;
            Vector3 bulletOrigin = transform.position + rb.transform.forward;
            spawnPos = bulletOrigin;

            Instantiate(magicCircle, npcLocation.position, npcLocation.rotation);
            Rigidbody bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity).GetComponent<Rigidbody>();

            float randomFactor = Random.Range(-10f, 10f);

            bullet.AddForce(rb.transform.forward * (bulletForce + randomFactor), ForceMode.Impulse);
            lastAttackTime = Time.time;
        }
        else Debug.Log("Cooldown on");

    }
}
