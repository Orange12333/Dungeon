using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector3 forward;
    Vector3 right;
    RaycastHit cameraRayHit;
    Ray cameraRay;
    public bool canMove = true;
    public Cast caster;
    public GManager manager;
    public AudioSource damageSound;

    //stats
    private float speed = 5f;
    private int maxHealth = 5;
    private int health = 5;
    private int damage = 5;

    public float hitRate = 0.5f;
    private float nextHit = 0.0f;

    private Animator animator;
    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GManager>();
        manager.RestartScena();
        animator = GetComponent<Animator>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        RefreshStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Move();
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                AttackInDirection();
            }
            else
            {
                animator.SetBool("isAttacking", false);
            }
        }
    }

    void AttackInDirection()
    {
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            animator.SetBool("isAttacking", true);
            Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
            transform.LookAt(targetPosition);
            caster.Shoot(targetPosition,damage);
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
        animator.SetBool("isMoving",true);
    }

    private void GetHit(int damage)
    {
        if (Time.time > nextHit)
        {
            damageSound.Play();
            nextHit = Time.time + hitRate;
            health -= damage;
            manager.getDamageInterface();
            if (health <= 0)
            {
                speed = 0;
                canMove = false;
                animator.SetTrigger("death");
                StartCoroutine(Death());
            }
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        manager.PlayerDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canMove)
        {
            if (other.CompareTag("EnemyHit"))
            {
                GetHit(other.GetComponent<EnemyHit>().getDamage());
                Destroy(other.gameObject);
            }
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int h)
    {
        health = h;
    }

    public void RefreshStats()
    {
        health = manager.getHealth();
        maxHealth = manager.getHealth();
        damage = manager.getDamage();
        speed = manager.getSpeed();
    }
}
