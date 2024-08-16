using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 20;
    public int damage = 1;

    public NavMeshAgent enemy;
    public Transform Player;
    public GameObject damageSound;

    public HitBoxSpawner spawner;
    private Animator animator;
    private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = FindObjectOfType<Player>().transform;
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(Player.position);
        if (health > 0)
        {
            if (Vector3.Distance(Player.position, this.transform.position) < 2)
            {
                animator.SetTrigger("attack");
                spawner.spawnHitBox(damage);
            }
        }
    }

    void getHit(int damage)
    {
        health -= damage;
        GameObject newSound = Instantiate(damageSound);
        newSound.GetComponent<AudioSource>().Play();
        Destroy(newSound, 1);
        if (health <= 0)
        {
            animator.SetBool("dead", true); 
            enemy.isStopped = true;
            collider.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Fireball"))
        {
            getHit(collision.collider.GetComponent<Fireball>().GetDamage());
        }
    }

    void Death()
    {
        DestroyObject(gameObject);
    }

    public int getDamage()
    {
        return damage;
    }
}
