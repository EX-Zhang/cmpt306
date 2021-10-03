using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctBoss : MonoBehaviour
{
    public int A;        //计时器
    public GameObject targetEnemy;      //该出生点生成的怪物  
    public GameObject targetbullet;      //该出生点生成的怪物   
    public int enemyTotalNum = 10;      //生成怪物的总数量    
    public float intervalTime = 30f;      //生成怪物的时间间隔   
    public float intervalTime1 = 60f;     
    public int enemyCounter;           //生成怪物的计数器
    private GameObject targetPlayer;    //玩家    
    public int health = 20;
    public GameObject door;
    public bool activate;
    public bool summonOnce;
    public float runtime = 0;


    public GameObject deatheffect;

    private bool cansummon;

    Animator anim;

    private int takedamage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
        targetPlayer = GameObject.FindGameObjectWithTag("Player");   //玩家        
        enemyCounter = 0;        //初始时，怪物计数为0
        cansummon = true;
        summonOnce = false;

    }

    // Update is called once per frame
    void Update()
    {
        activate = door.GetComponent<swtichBGMTrigger>().bossactivate;

        if (health < 0)
        {
            death();
        }
        else if (activate)
        {
            if (summonOnce == false)
            {
                InvokeRepeating("summon", 10, 1f);
                summonOnce = true;
            }
            move();
            if(runtime == intervalTime1 && targetPlayer!= null)
            {
                InvokeRepeating("attack", 3F, intervalTime);
                runtime = 0;
            }
            runtime++;


        }
        A++;


    }

    void move(){
            if(A<200){
                transform.position=new Vector3(transform.position.x,transform.position.y+3f*Time.deltaTime,0);
            }
            if(A>200&&A<400){
                transform.position=new Vector3(transform.position.x,transform.position.y-3f*Time.deltaTime,0);
            }
            if(A>400){
                A =0;
                summon();
            }
    }
    void attack(){
        Instantiate(targetbullet, this.transform.position, Quaternion.identity);
    }


    void summon(){
        Instantiate(targetEnemy, this.transform.position, Quaternion.identity); //生成一只怪物          
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Bullet")
        {
            takedamage = collision.gameObject.GetComponent<Bullet>().damage;
            health = health - takedamage;
        }
    }
    void death(){
        Destroy(gameObject, 1f);
        anim.Play("octbossdeath");
    }
}
