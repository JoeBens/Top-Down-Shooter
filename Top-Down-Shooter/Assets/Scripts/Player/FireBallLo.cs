using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallLo : Projectile
{

    public bool isSplitable;

    public Transform dir1, dir2;

    private bool instantiate = true;

    public GameObject fireLoSmall, fireLoSmall2;

    public void Split()
    {
        if (isSplitable == true && instantiate == true)
        {
            Instantiate(fireLoSmall, transform.position, dir1.rotation);
            Instantiate(fireLoSmall2, transform.position, dir2.rotation);
            instantiate = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "test")
        {
            Split();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            
            Split();
            DestroyProjectile();
        }

        
    }
}
