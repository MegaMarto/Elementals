using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleLifetime : MonoBehaviour
{
    public GameObject meleObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(meleObject);
    }
}
