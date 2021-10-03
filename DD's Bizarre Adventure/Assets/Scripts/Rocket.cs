using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Rocket : MonoBehaviour
{
    public AIPath aipath;
    public GameObject impactEffect;
    public GameObject Target;
    public int explotionrange;
	public int damage = 40;

    private Vector2 cupositon;


    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0,-180,90);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3);
        // will destroy after 3 seconds.
        aipath.destination = Target.transform.position;
        if (aipath.desiredVelocity.x >= 0.01f) {
            transform.eulerAngles = new Vector3(0,-180,90);
        }
        else if (aipath.desiredVelocity.x < -0.01f){
            transform.eulerAngles = new Vector3(0,0,90);
        }

    }


    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),collision.gameObject.GetComponent<Collider2D>());
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
            cupositon = transform.position;
            cupositon.x += Random.value *explotionrange;
            cupositon.x -= Random.value *explotionrange;

            cupositon.y += Random.value *explotionrange;
            cupositon.y -= Random.value *explotionrange;

            Instantiate(impactEffect, cupositon, Quaternion.identity);
            }
		    Destroy(gameObject);

	    }
    }
}
