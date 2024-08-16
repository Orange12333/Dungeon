using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFloor : MonoBehaviour
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
            manager.EndFloor();
        }
    }
}
