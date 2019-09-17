using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	
	public float speed;
	
	private Rigidbody2D rb;
	
	private Animator playerAnim;

    public int health;


	private Vector2 move;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
		playerAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");
		
		move = new Vector2(moveX, moveY) * speed * Time.deltaTime;
		
		if(move != Vector2.zero){
			playerAnim.SetBool("isRunning", true);
            
		}
		else{
			playerAnim.SetBool("isRunning", false);
		}
		
    }
	void FixedUpdate(){
		rb.MovePosition(rb.position + move);
	}


    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
