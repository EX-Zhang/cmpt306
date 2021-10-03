using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ebulletcontroller : MonoBehaviour
{

    public float speed;
    Rigidbody2D rb;
    [SerializeField] GameObject target;
    Vector2 movedir;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        if (target == null){
            return;
        }
        movedir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2 (movedir.x,movedir.y);
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy"){
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),collision.gameObject.GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Bullet") {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer ==8) {
            Destroy(gameObject);
        }
    }
}
