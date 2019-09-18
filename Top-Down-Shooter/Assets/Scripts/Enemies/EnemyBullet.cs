using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector]
    public Transform player;

    public float speed;

    public Vector2 targetPos;

    public float lifeTime;

    public GameObject explosion;

    public int damage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("DestroyProjectile", lifeTime);
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


    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
