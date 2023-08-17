using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeathTarget : MonoBehaviour
{

    //private float dist;
    //private Transform enemyPos;
    public bool attacking;

    public GameObject captureTarget;
    public GameObject target;
    //public GameObject[] enemies;
    //public GameObject[] capturePoints;
    public List<GameObject> capturePoints = new List<GameObject>();

    //public float distGap;

    public List<GameObject> enemyDictionary = new List<GameObject>();

    //private GameObject[] team_1;
    //private GameObject[] team_2;
    //private GameObject[] team_4;
    //private GameObject[] team_5;
    //private GameObject[] team_6;

    public Collider[] enemiesInsideZone;


    // Start is called before the first frame update
    void Start()
    {
        capturePoints.AddRange(GameObject.FindGameObjectsWithTag("CapturePoint"));

        captureTarget = capturePoints[Random.Range(0, capturePoints.Count)];

        enemyDictionary.AddRange(GameObject.FindGameObjectsWithTag("Team1"));
        enemyDictionary.AddRange(GameObject.FindGameObjectsWithTag("Team2"));
        enemyDictionary.AddRange(GameObject.FindGameObjectsWithTag("Team4"));
        enemyDictionary.AddRange(GameObject.FindGameObjectsWithTag("Team5"));
        enemyDictionary.AddRange(GameObject.FindGameObjectsWithTag("Team6"));

        //for (int i = 0; i < enemyDictionary.Count; i++)
        //{
        //    for (int e = 0; e < enemyDictionary[i].Length; e++)
        //    {
        //        Debug.Log(enemyDictionary[i][e]);
        //    }
        //}
        //team_2 = GameObject.FindGameObjectsWithTag("Team2");
        //team_4 = GameObject.FindGameObjectsWithTag("Team4");
        //team_5 = GameObject.FindGameObjectsWithTag("Team5");
        //team_6 = GameObject.FindGameObjectsWithTag("Team6");

        //enemies = team_1.Concat(team_1).Concat(team_2).Concat(team_4).Concat(team_5).Concat(team_6).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesInsideZone = Physics.OverlapSphere(this.transform.position, 10f, ~LayerMask.GetMask("Terrain", "Bullet"));

        foreach (Collider enemy in enemiesInsideZone)
        {
            if (enemyDictionary.Contains(enemy.gameObject))
            {
                target = enemy.gameObject;
                attacking = true;
            } else if (!enemyDictionary.Contains(enemy.gameObject)) {
                target = captureTarget;
                attacking = false;
            }
            //else if(!enemyDictionary.Contains(enemy.gameObject))
            //{
            //    //target = capturePoints[Random.Range(0, 6)];
            //    attacking = false;
            //}

        }
        //dist = Vector3.Distance(enemyPos.position, transform.position);

        //selectTarget();
    }

    //void selectTarget()
    //{
    //    if (target == null && dist >= distGap)
    //    {
    //        target = capturePoints[Random.Range(0, 6)];
    //    }
    //    else if (target != null && dist <= distGap)
    //    {
    //        target = enemies;
    //    }
    //    else if (target == enemies && dist >= distGap)
    //    {
    //        target = capturePoints[Random.Range(0, 6)];
    //    } else if (target == null && dist <= distGap)
    //    {
    //        target = enemies;
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}