using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float bulletForce;
    [SerializeField] private float wallRange;
    public float cooldownTime = 2;
    private float lastAttackTime;
    private float lastWallTime;
    public Camera cam;

    void Start()
    {
        lastAttackTime = Time.time;
        lastWallTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LaunchBullet();

        }
        else if (Input.GetMouseButtonDown(1))
        {
            SpawnWall();
        }

    }


    private void SpawnWall()
    {
        if (Time.time - lastWallTime > cooldownTime)
        {
            RaycastHit hit;
            Ray wallRay = new Ray(transform.position, cam.transform.forward);


            if (Physics.Raycast(wallRay, out hit))
            {


                if (hit.distance < wallRange)
                {
                    Vector3 spawnPos = hit.point;
                    spawnPos.y += 3;

                    GameObject wall = Instantiate(wallPrefab, spawnPos, transform.rotation);
                }
            }
            lastWallTime = Time.time;
        }

    }

    public void LaunchBullet()
    {
        if (Time.time - lastAttackTime > cooldownTime)
        {
            Debug.Log("Cooldown off");
            Vector3 spawnPos = transform.position;
            Vector3 bulletOrigin = transform.position + cam.transform.forward;
            spawnPos = bulletOrigin;

            Rigidbody bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity).GetComponent<Rigidbody>();

            float randomFactor = Random.Range(-10f, 10f);

            bullet.AddForce(cam.transform.forward * (bulletForce + randomFactor), ForceMode.Impulse);
            lastAttackTime = Time.time;
        }
        else Debug.Log("Cooldown on");

    }
}
