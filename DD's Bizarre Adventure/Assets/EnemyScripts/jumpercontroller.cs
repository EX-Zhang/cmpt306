using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpercontroller : MonoBehaviour
{
    public float jumperVelocity;
    public float waittime;
    public float speed;
    public GameObject EnemyDeathEffect;
    public GameObject Healthpack;

    public float healthpacgenerationrate = 0.2f;

    [SerializeField] private LayerMask playerMask;
    Vector2 target;
    Rigidbody2D rb;
    Renderer render;
    GameObject Player;
    BoxCollider2D BoxCollider;
    private bool canjump;
    public int heath;
    private int takedamage;



    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        render = GetComponent<Renderer>();
        BoxCollider = transform.GetComponent<BoxCollider2D>();
        Player = GameObject.FindWithTag("Player");
        canjump = true;
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
        if (!canjump) {
            return;
        }
        target = Player.transform.position;
        if (IsGrounded() && render.isVisible){
            StartCoroutine(wait());
        }

    }

    bool IsGrounded(){
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size,0f,Vector2.down,.1f, playerMask);
        return raycastHit2d.collider !=null;
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

    IEnumerator wait(){
        gameObject.transform.Translate(Vector2.zero * Time.deltaTime);
        canjump = false;
        anim.SetBool("jump",false);
        yield return new WaitForSeconds(waittime);
        anim.SetBool("jump", true);
        canjump = true;
        if (target.x > gameObject.transform.position.x)
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * jumperVelocity, ForceMode2D.Impulse);
        }

        else
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * jumperVelocity, ForceMode2D.Impulse);
        }
    }
}
