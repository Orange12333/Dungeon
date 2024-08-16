using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Vector3 shootdir;
    private int damage;
    public void Setup(Vector3 shootDirection, int damageAmount)
    {
        shootdir = shootDirection;
        this.damage = damageAmount;
    }

    private void Start()
    {

    }

    private void Update()
    {
        float speed = 10f;
        shootdir.y = 0;
        transform.position += shootdir * speed/(Mathf.Sqrt(Mathf.Pow(shootdir.x,2)+ Mathf.Pow(shootdir.z, 2))) * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyObject(gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }
}
