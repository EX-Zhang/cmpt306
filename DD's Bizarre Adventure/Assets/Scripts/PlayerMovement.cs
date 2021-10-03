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
	public bool jump = false;
    private bool invincible = false;
    int count = 0;
    // Update is called once per frame
    void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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

        if (collider.gameObject.CompareTag("Ebullet") && !invincible)
        {
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

        if (collision.gameObject.CompareTag("Ebullet") && !invincible)
        {
            {
                HeartHealthVisual.heartHealthSystemStatic.Damage();
                invincible = true;
                Invoke("resetInvulnerability", 2);
            }
        }
    }

    void resetInvulnerability()
    {
        invincible = false;
    }
}
