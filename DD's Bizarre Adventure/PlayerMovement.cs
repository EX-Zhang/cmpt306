using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
    public PrefabWeapon weapon;                           
    public float runSpeed = 40f;

    public int inihealth = 3;              // initial health start at 3
    float horizontalMove = 0f;
	bool jump = false;
    private bool invincible = false;
    int count = 0;

    public static int shoot_id = -1;
    int right_move = -1;
    int left_move = -1;
    int jump_id = -1;
    bool jump_check = false;
    

    // Update is called once per frame
    void Update () {

        /*horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}

        // if want to shoot
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("IsShooting", true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("IsShooting", false);
        }*/

        if (Input.touchCount == 0)
        {
            horizontalMove = 0;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            animator.SetBool("IsShooting", false);
            left_move = -1;
            right_move = -1;
            shoot_id = -1;
            jump_id = -1;
        }


        for(int i = 0; i < Input.touchCount; i++)
        {
            float mid = Screen.width / 2;
            if (left_move==-1&&Input.GetTouch(i).position.x < mid/2 && Input.GetTouch(i).position.x>mid/4)
            {
                right_move = i;
            }
            else if (right_move==-1&&  Input.GetTouch(i).position.x<mid/4)
            {
                left_move = i;
            }
            if (Input.GetTouch(i).position.x > mid*3/2)
            {
                float height_mid = Screen.height / 2;
                if (Input.GetTouch(i).position.y<2* Screen.height /3&& Input.GetTouch(i).position.y> Screen.height/3)
                {
                    jump_id = i;
                }

                if (shoot_id == -1&& Input.GetTouch(i).position.y < Screen.height/3)
                {
                    shoot_id = i;
                }
            }
            
        }


        if (right_move != -1)
        {
            Touch move = Input.GetTouch(right_move);
            if (move.phase == TouchPhase.Began)
            {
                horizontalMove = 1 * runSpeed;
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }
            else if(move.phase == TouchPhase.Ended)
            {
                horizontalMove = 0;
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
                if (shoot_id != -1&& shoot_id != 0)
                {
                    shoot_id = right_move;
                }
                right_move = -1;
            }
        }
        else if (left_move != -1)
        {
            Touch move = Input.GetTouch(left_move);
            if (move.phase == TouchPhase.Began)
            {
                horizontalMove = -1 * runSpeed;
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            }
            else if (move.phase == TouchPhase.Ended)
            {
                horizontalMove = 0;
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
                if (shoot_id != -1 && shoot_id != 0)
                {
                    shoot_id = left_move;
                }
                left_move = -1;
            }
        }

        if (jump_id != -1)
        {
            Touch jump_touch = Input.GetTouch(jump_id);
            if (jump_touch.phase == TouchPhase.Began)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            else if (jump_touch.phase == TouchPhase.Ended)
            {
                jump_id = -1;
            }
        }
        if (shoot_id != -1)
        {
            Touch shoot_touch = Input.GetTouch(shoot_id);
            if (shoot_touch.phase == TouchPhase.Began)
            {
                animator.SetBool("IsShooting", true);
            }
            else if (shoot_touch.phase == TouchPhase.Ended)
            {
                animator.SetBool("IsShooting", false);
                shoot_id = -1;
            }
        }
    }

	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}

    public void DamageKnockBack(Vector3 knockbackdir, float distance)
    {
        transform.position += knockbackdir * distance;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Heal"))
        {
            HeartHealthVisual.heartHealthSystemStatic.Heal();
        }

        if (collider.gameObject.CompareTag("Double Jump"))
        {
            controller.maxjump = 2;
        }
        if (collider.gameObject.CompareTag("upgrade"))
        {
            weapon.UpdateWeapon();
        }

        if (collider.gameObject.CompareTag("Enemy") && !invincible){
            {
                HeartHealthVisual.heartHealthSystemStatic.Damage();
                invincible = true;
                Invoke("resetInvulnerability", 2);
            }
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Enemy") && !invincible)
            {
                HeartHealthVisual.heartHealthSystemStatic.Damage();
                invincible = true;
                Invoke("resetInvulnerability", 2);
            }
    }

    void resetInvulnerability()
    {
        invincible = false;
    }

}
