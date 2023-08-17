using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float health = 100;
    public Rigidbody self;
    public HealthBar healthBar;

    [SerializeField] private Transform canvasTransform;

    public void TakeDamage()
{
    health -= 50;
    if (health <= 0)
    Dead();
}

    private void LateUpdate()
    {
        canvasTransform.LookAt(Camera.main.transform);
    }

    void Dead()
{
    transform.position = new Vector3(6,0,0);
        //Instantiate(self);
        //Destroy(gameObject);
        health = 100;
}

private void OnTriggerEnter(Collider other)
{
if(other.gameObject.CompareTag("MeleTag"))
TakeDamage();
healthBar.SetHealth(health);
}
}
