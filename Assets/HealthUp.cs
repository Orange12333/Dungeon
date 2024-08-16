using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    private GManager manager;
    private DamageUp damageUp;
    private SpeedUp speedUp;
    private void Start()
    {
        manager = FindObjectOfType<GManager>();
        damageUp = FindObjectOfType<DamageUp>();
        speedUp = FindObjectOfType<SpeedUp>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            manager.increaseHealth();
            DestroyObject(gameObject);
            DestroyObject(speedUp.gameObject);
            DestroyObject(damageUp.gameObject);
        }
    }
}
