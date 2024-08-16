using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private GManager manager;
    private DamageUp damageUp;
    private HealthUp healthUp;
    private void Start()
    {
        manager = FindObjectOfType<GManager>();
        damageUp = FindObjectOfType<DamageUp>();
        healthUp = FindObjectOfType<HealthUp>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            manager.increaseSpeed();
            DestroyObject(gameObject);
            DestroyObject(healthUp.gameObject);
            DestroyObject(damageUp.gameObject);
        }
    }
}
