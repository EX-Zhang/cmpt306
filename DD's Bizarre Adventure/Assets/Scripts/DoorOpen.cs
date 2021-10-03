using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private int A;
    private GameObject targetPlayer;
    private bool dooropen;
    private Vector3 initposition;

    public bool bossactivate;
    public GameObject boss;


    public int height;
    public AudioClip newTrack;
    public AudioClip newTrack1;
    private AudioManager AM;
    private bool change = true;



    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player"); 
        A= 0;
        initposition = new Vector3 (transform.position.x,transform.position.y,0);
        bossactivate = false;
        AM = FindObjectOfType<AudioManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null){
            if (dooropen && targetPlayer.transform.position.x - transform.position.x < 0){
                move();
            }
            if (targetPlayer.transform.position.x - transform.position.x > 3){
                doorclose();
                bossactivate = true;
            }
        }
        else
        {
            A = 0;
            dooropen = true;
            if (dooropen){
                move();
            }
            if (newTrack1 != null && change)
            {
                AM.changeBGM(newTrack1);
                change = false;
            }

        } 
        
        
    }


    void move(){
        if(transform.position.y < initposition.y+height){
            transform.position=new Vector3(transform.position.x,transform.position.y+3f*Time.deltaTime,0);
        }
        if (transform.position.y >= initposition.y+height){
            dooropen = false;
        }
    }


    void doorclose(){
        if (transform.position.y > initposition.y){
            transform.position=new Vector3(transform.position.x,transform.position.y-3f*Time.deltaTime,0);
        }
        if (newTrack != null)
        {
            AM.changeBGM(newTrack);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            dooropen = true;
        }
    }

}
