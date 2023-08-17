using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLifetime : MonoBehaviour
{
    public GameObject WallObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(25f);
        Destroy(WallObject);
    }
}
