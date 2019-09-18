using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance;

    public float attackCd;

    public Animator anim;

    public Transform shootPosition;

    public GameObject bullet;

    public override void Start()
    {
        base.Start();
        anim = this.GetComponent<Animator>();
    }
    private void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if(Time.time >= attackCd)
            {
                attackCd = Time.time + timeBetweenShots;
                anim.SetTrigger("shoot");
            }

        }
    }

    public void Shoot()
    {
        Vector2 direction = player.position- shootPosition.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shootPosition.rotation = rotation;


        Instantiate(bullet, shootPosition.position, shootPosition.rotation);
    }

}
