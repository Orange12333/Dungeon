using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour
{
    private GManager manager;
    private SpeedUp speedUp;
    private HealthUp healthUp;
    private void Start()
    {
        manager = FindObjectOfType<GManager>();
        speedUp = FindObjectOfType<SpeedUp>();
        healthUp = FindObjectOfType<HealthUp>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            manager.increaseDamage();
            DestroyObject(gameObject);
            DestroyObject(healthUp.gameObject);
            DestroyObject(speedUp.gameObject);
        }
    }
}
