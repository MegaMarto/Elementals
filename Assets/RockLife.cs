using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLife : MonoBehaviour
{
    public GameObject Rock;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(Rock);
    }
}
