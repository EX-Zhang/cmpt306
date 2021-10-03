using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public int health = 10;
    public float timer = 10f;
    public float timerCount;
    public float turncount = 2f;
    public float speed = 0.1f;
    public float range = 3f;
    public Transform target;
    public Transform lance;
    private bool hit = false;
    public float direction;
    public bool faceright;
    public bool cooling;
    public float duration = 2f;
    public bool turning;
    private bool activate;
    public GameObject door;
    public float maxRange = 227;
    public float minRange = 173;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag(playerTag);
        anim = GetComponent<Animator>();     
        faceright=true;
        cooling=true;
        timerCount=timer;
        activate = false;


    }

    // Update is called once per frame
    void Update()
    {
        activate = door.GetComponent<DoorOpen>().bossactivate;
        if (activate)
        {
            //timerCount -= Time.deltaTime;
            if( health <= 0)
            {
                death();
            }
            else{

                if (cooling)
                {
                    timerCount -= Time.deltaTime;
                    rotation();
                   // if (timerCount <= 1)
                   // {
                    //    anim.Play("e4hit");
                    //}


                    if (timerCount <= 0)
                    {
                        cooling = false;
                    }
                    if (turning)
                    {
                        turncount -= Time.deltaTime;
                        if (turncount <= 0)
                        {
                            turning = false;
                            turncount = 2f;
                        }
                    }
                }
                attack2();
                if (hit == true)
                {
                    Vector3 temp = new Vector3(1f, 0, 0);
                    lance.position += temp;
                    hit = false;
                }

                //if (Vector3.Distance(transform.position, target.position) < range)
                //{
                //  attack1();
                //}

                if ((Vector3.Distance(transform.position, target.position) > range) && (Vector3.Distance(transform.position, target.position) < 10 * range) && !turning)
                {
                    move();
                }
            }
        }
       
    }


    void move(){
        // if((Vector3.Distance(transform.position,target.position)>range) && (Vector3.Distance(transform.position,target.position)<10)){
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
            anim.Play("e4walk");
        // }
        
    }

    void attack1(){
        //if(Vector3.Distance(transform.position,target.position)<5f){
            anim.Play("e4attack1");
        //}
    }

    void attack2(){
        //timer -= Time.deltaTime;
        //if(Vector3.Distance(transform.position,target.position)<5f) && (timer<=0))){
            if(!cooling){
            
                anim.Play("e4attack2");
                transform.Translate(Vector2.right * 2 * speed * direction * Time.deltaTime);
                timerCount+=Time.deltaTime;
                if(timerCount>=timer/4){
                    timerCount=timer;
                    cooling=true;
                    
                }
                
            }
            else if (Vector3.Distance(transform.position, target.position) < range)
            {
                anim.Play("e4attack1");
                Vector3 temp = new Vector3(1f, 0, 0);
                lance.position -= temp;
                hit = true;

        }
        // anim.Play("e4attack2");
        // transform.Translate(Vector2.right * 2 * speed * direction * Time.deltaTime);
        //}
    }

    void rotation(){
    direction = Vector3.Dot(transform.right, target.position-transform.position); 
        if(direction>0){
            direction=1;
        }
        if(direction<0){
            direction=-1;
        }
        if(direction<0 && faceright){
            //direction=-1;
           Flip();
             //faceright=true;
            
        }
        if(direction>0 && !faceright){
            //direction=1;
           Flip();
           faceright=true;
           
        }
    }

    void death(){
        //if(health<=0){
            Destroy(gameObject, 1f);
            anim.Play("e4dead");
        //}
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Bullet"&& activate){
            health--;
        }
    }

    void Flip(){
        faceright = !faceright;
        Vector3 scale = transform.localScale;
        turning = true;
        scale.x *= -1;
        transform.localScale = scale;

        if (transform.position.x > maxRange && scale.x > 0)
        {
            transform.position = transform.position + new Vector3(-3, 0, 0);
        }
        if (transform.position.x < minRange && scale.x < 0)
        {
            transform.position = transform.position + new Vector3(5, 0, 0);
        }

    }
}
