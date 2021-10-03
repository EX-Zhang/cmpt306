using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class octopuscontroller: MonoBehaviour
{
    public AIPath aipath;
    Renderer render;
    public GameObject EnemyDeathEffect;
    public int heath;
    private int takedamage;


    void start() {
        render = GetComponent<Renderer>();
        aipath.canSearch = false;
        aipath.canMove = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (heath <= 0){
            Instantiate(EnemyDeathEffect, transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        if (aipath.desiredVelocity.x >= 0.01f) {
            transform.eulerAngles = new Vector3(0,-180,0);
        }
        else if (aipath.desiredVelocity.x <= -0.01f){
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Enemy"){
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),collision.gameObject.GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Bullet")
        {
            takedamage = collision.gameObject.GetComponent<Bullet>().damage;
            heath = heath - takedamage;
        }
    }


    void OnBecameVisible() {
        aipath.canSearch = true;
        aipath.canMove = true;
    }
}
