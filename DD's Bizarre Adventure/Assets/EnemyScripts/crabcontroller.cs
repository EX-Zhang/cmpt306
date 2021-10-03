using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crabcontroller : MonoBehaviour
{
    public float speed;
    public float distance;
    public float waittime;
    public bool moveright = true;
    private string movedir;
    private bool move;
    public int heath;
    Renderer render;
    public GameObject EnemyDeathEffect;
    public GameObject Healthpack;

    public float healthpacgenerationrate = 0.2f;


    private int takedamage;
    [SerializeField] private LayerMask platformlayermask;

    public Transform groundDetection;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {   move = true;
        anim = GetComponent<Animator>();
        render = GetComponent<Renderer>();
        if (moveright == true){
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            movedir = "right";
        }
        else
        {
            movedir = "left";
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
        if (collision.gameObject.layer ==8) {
            StartCoroutine(wait());
            if (moveright == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                Flip();
            }
            else
            {
                Flip();
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (heath <= 0)
        {
            Instantiate(EnemyDeathEffect, transform.position, Quaternion.identity);
            if (Random.value < healthpacgenerationrate)
            {
                Instantiate(Healthpack, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        if (!move || !render.isVisible)
        {
            return;
        }
        if (movedir == "right")
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        anim.SetFloat("speed", speed);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance,platformlayermask);

        if (groundInfo.collider == false)
        {
            StartCoroutine(wait());
            if (moveright == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                Flip();
            }
            else
            {
                Flip();
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }


    void  Flip(){
        moveright = !moveright;
    }


    IEnumerator wait(){
        move = false;
        anim.SetFloat("speed",0);
        yield return new WaitForSeconds(waittime);
        anim.SetFloat("speed",speed);
        move = true;
    }

}
