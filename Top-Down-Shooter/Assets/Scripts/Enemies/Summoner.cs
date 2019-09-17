using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX, maxX, minY, maxY;

    public Vector2 summonPosition;

    private Animator anim;

    public GameObject spawn;

    private float summonTime;

    public float attackSpeed;

    public float stopDistance;

    public float attackCd;

    public override void Start()
    {
        base.Start();

        anim = this.GetComponent<Animator>();

        float randomX = Random.Range(minX,maxX);
        float randomY = Random.Range(minY, maxY);
        summonPosition = new Vector2(randomX, randomY);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if(Vector2.Distance(transform.position, summonPosition) >.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, summonPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);

                if(Time.time >= summonTime)
                {
                    //Instantiate(spawn, summonPosition, Quaternion.identity);

                    summonTime = Time.time + timeBetweenShots;
                    anim.SetTrigger("summon");
                }
            }

            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time >= attackCd)
                {
                    //attack
                    StartCoroutine(Attack());
                    attackCd = Time.time + timeBetweenShots;
                }
            }

        }
    }

    public void SummonSpawn()
    {
        if (player != null)
            Instantiate(spawn, summonPosition, Quaternion.identity);
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;

            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }

}
