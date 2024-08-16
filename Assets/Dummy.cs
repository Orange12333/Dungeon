using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dummy : MonoBehaviour
{
    private Animator animator;
    public GManager manager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<GManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Fireball"))
        {
            animator.SetTrigger("Damage");
        }
    }
}
