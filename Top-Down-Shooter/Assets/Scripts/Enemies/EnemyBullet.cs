using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Projectile
{
    [HideInInspector]
    public Transform player;
    public Vector2 targetPos;



    public override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        targetPos = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if( player != null)
        {
            if(Vector2.Distance(transform.position, player.position ) > .1f)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                DestroyProjectile();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);

            DestroyProjectile();
        }
    }

}
