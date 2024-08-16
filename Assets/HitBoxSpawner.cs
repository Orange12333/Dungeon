using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxSpawner : MonoBehaviour
{
    public GameObject hitbox;
    public float firerate = 0.5f;
    private float nextFire = 0.0f;
    public void spawnHitBox(int damage)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + firerate;
            GameObject newFireball = Instantiate(hitbox, this.transform.position, Quaternion.identity);
            Vector3 shootPos = this.transform.position;
            newFireball.transform.rotation = this.transform.rotation;
            newFireball.GetComponent<EnemyHit>().setDamage(damage);
            Destroy(newFireball, 2);
        }
    }
}
