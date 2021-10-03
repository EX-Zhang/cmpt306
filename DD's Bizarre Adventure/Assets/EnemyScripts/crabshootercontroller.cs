using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crabshootercontroller : MonoBehaviour
{   
    [SerializeField] GameObject Ebullet;
    [SerializeField] GameObject player;
    Renderer render;
    public float firerate;
    float nextfire;
    Animator anim;
    Vector2 target;
    public int heath;
    public GameObject EnemyDeathEffect;
    public GameObject Healthpack;

    public float healthpacgenerationrate = 0.2f;
    private int takedamage;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        render = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        nextfire = Time.time;
        if (player == null){
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (heath <= 0){
            Instantiate(EnemyDeathEffect, transform.position,Quaternion.identity);
            if (Random.value < healthpacgenerationrate) {
                Instantiate(Healthpack, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        target = player.transform.position;
        if (target.x > gameObject.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (render.isVisible)
        {
            fire();
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

    void fire(){
        anim.SetBool("shoot",false);
        if (Time.time > nextfire){
            anim.SetBool("shoot",true);
            Instantiate (Ebullet, transform.position, Quaternion.identity);
            nextfire = Time.time + firerate;            
        }
    }
}
