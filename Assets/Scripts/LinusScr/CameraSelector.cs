using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    private float timer;
    public List<GameObject> npcs = new List<GameObject>();
    private GameObject npc;
    //public Transform[] playerTransforms;
    public Transform camTransform;
    int randomPlayer;
    //public float yOffset = 1f;
    //public float distanceZ;
    //public float distanceX;
    //public float minDistanceX = 10f;
    //public float minDistanceZ = 10f;

    //private float xMin, xMax, yMin, yMax, zMin, zMax;
    private void Start()
    {
        randomPlayer = Random.Range(0, npcs.Count);
        
        timer = 1;
        npcs.AddRange(GameObject.FindGameObjectsWithTag("Team1"));
        npcs.AddRange(GameObject.FindGameObjectsWithTag("Team2"));
        npcs.AddRange(GameObject.FindGameObjectsWithTag("Team3"));
        npcs.AddRange(GameObject.FindGameObjectsWithTag("Team4"));
        npcs.AddRange(GameObject.FindGameObjectsWithTag("Team5"));
        npcs.AddRange(GameObject.FindGameObjectsWithTag("Team6"));
    }
    private void Update()
    {
        npc = npcs[randomPlayer];
        FollowCam();
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            randomPlayer = Random.Range(0, npcs.Count);
            Debug.Log(npc);
            timer = 5;
        }
    }
    private void FollowCam()
    {
        //camTransform = transform.GetComponent<Camera>;
        camTransform.position = transform.position;

        //v3 Campos
        Vector3 newCamPos;
        //Making camPos same as players
        newCamPos = new Vector3(npc.transform.position.x, 20, npc.transform.position.z- 10  );
        camTransform.position = newCamPos;

    }
}

//Ignore this, old code, didn't work as planned
//    private void LateUpdate()
//    {
//        if (playerTransforms.Length == 0)
//        {
//            print("No players assigned.");
//            return;
//        }

//        xMin = xMax = playerTransforms[0].position.x;
//        yMin = yMax = playerTransforms[0].position.y;
//        zMin = zMax = playerTransforms[0].position.z;

//        for (int i = 1; i < playerTransforms.Length; i++)
//        {
//            if (playerTransforms[i].position.x < xMin) { xMin = playerTransforms[i].position.x; }
//            if (playerTransforms[i].position.x > xMax) { xMax = playerTransforms[i].position.x; }
//            if (playerTransforms[i].position.y < yMin) { yMin = playerTransforms[i].position.y; }
//            if (playerTransforms[i].position.y > yMax) { yMax = playerTransforms[i].position.y; }
//            if (playerTransforms[i].position.z < zMin) { zMin = playerTransforms[i].position.z; }
//            if (playerTransforms[i].position.z > zMax) { zMax = playerTransforms[i].position.z; }

//        }

//        float xMiddle = (xMin + xMax) /2;
//        float yMiddle = (yMin + yMax) ;
//        float zMiddle = (zMin + zMax) /2;
//        distanceX = xMax - xMin;
//        distanceZ = zMax - zMin;

//        if (distanceX < minDistanceX) { distanceX = minDistanceX; }
//        if (distanceZ < minDistanceZ) { distanceZ = minDistanceZ; }

//        transform.position = new Vector3(xMiddle, yMiddle + (distanceX / 3) + (distanceZ / 3), zMiddle - (distanceX / 3) - (distanceZ / 3));
//        }
//}
