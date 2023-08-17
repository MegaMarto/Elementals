using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionExpansion : MonoBehaviour
{
    public GameObject explosion;

    public float timer = 0f;
    public float growTime = 6f;
    public float maxSize = 2f;

    public bool isMaxSize = false;
    // Start is called before the first frame update
    void Start()
    {
        if(isMaxSize == false)
        {
            StartCoroutine(Grow());
        }
    }

    // Update is called once per frame
    private IEnumerator Grow()
    {
        Vector3 startScale = transform.localScale;
        Vector3 maxScale = new Vector3(maxSize, maxSize, maxSize);

        do
        {
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer/growTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < growTime);

        isMaxSize = true;
        
        if (isMaxSize = true)
        {
            Destroy(explosion);
        }
        
    }
}
