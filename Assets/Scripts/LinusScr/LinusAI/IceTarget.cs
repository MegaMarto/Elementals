using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTarget : MonoBehaviour
{
    private List<GameObject> capPoints = new List<GameObject>();
    [SerializeField] private GameObject capPoint;
    [SerializeField] private GameObject target;


    void Start()
    {
        capPoints.AddRange(GameObject.FindGameObjectsWithTag("CapturePoint"));
        int randomCap = Random.Range(0, capPoints.Count);
        capPoint = capPoints[randomCap];

    }


    public GameObject FoundCapPoint()
    {
        return capPoint;
    }
   
}
