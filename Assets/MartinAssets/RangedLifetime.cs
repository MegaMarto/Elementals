using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedLifetime : MonoBehaviour
{
    public GameObject rangedObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(15f);
        Destroy(rangedObject);
    }
}
