using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 15f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;
    public int destoryafter = 0;

	// Use this for initialization
	void Start () {
		rb.velocity = transform.right * speed;
        Destroy(gameObject, 1);
    }


	void OnCollisionEnter2D(){
		Instantiate(impactEffect, transform.position, Quaternion.identity);

		Destroy(gameObject,destoryafter);
        
	}
}
