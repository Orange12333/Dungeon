using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{
    public GameObject fireball;
    public float firerate = 0.5f;
    private float nextFire = 0.0f;
    public GameObject fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shoot(Vector3 shootDir, int damage)
    {
        if (Time.time > nextFire)
        {
            GameObject newFire = Instantiate(fire);
            AudioSource fireSound = newFire.GetComponent<AudioSource>();
            fireSound.Play();
            Destroy(newFire,1);
            nextFire = Time.time + firerate;
            GameObject newFireball = Instantiate(fireball,this.transform.position, Quaternion.identity);
            Vector3 shootPos = this.transform.position;
            newFireball.transform.rotation = Quaternion.LookRotation(new Vector3((shootDir - shootPos).x, 0.8f, (shootDir - shootPos).z));
            newFireball.GetComponent<Fireball>().Setup(shootDir-shootPos, damage);
            Destroy(newFireball, 2);
        }
    }
}
