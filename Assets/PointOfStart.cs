using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfStart : MonoBehaviour
{
    private GManager manager;
    void Start()
    {
        manager = FindObjectOfType<GManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.StartFloor();
        }
    }
}
