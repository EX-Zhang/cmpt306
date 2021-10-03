using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private float FillAmount;
    public Image fillimage;
    public float timer;
    public float increaseamount;
    public GameObject player;
    public GameObject rocket;

    private GameObject[] Enemy;

    public Camera cam;

    private bool needlaunch;
    private bool canlaunch;


    private int enemysize;
    private GameObject[] Enemys;




    private bool ready;
    


    // Start is called before the first frame update
    void Start()
    {
        FillAmount = 0f;
        ready = true;
        needlaunch = false;
        canlaunch = false;
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        enemysize = Enemys.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            StartCoroutine(handlechange());
        }

        if (needlaunch){
            foreach (GameObject t in Enemy){

            Vector3 viewPos = cam.WorldToViewportPoint(t.transform.position);

            if (viewPos.x< 1 && viewPos.x > 0 && viewPos.y < 1 && viewPos.y > 0)
                {
                canlaunch = true;
                }
            }
        }
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");

        if (Enemys.Length < enemysize && FillAmount < 1){
            FillAmount += 0.1f;
            fillimage.fillAmount = FillAmount;
            enemysize = Enemys.Length;
        }
        if (Enemys.Length > enemysize){
            enemysize = Enemys.Length;
        }



    }


    IEnumerator handlechange()
    {
        ready = false;
        yield return new WaitForSeconds(timer);
        FillAmount += increaseamount;
        if (FillAmount > 1)
        {
            StartCoroutine(rocketgeneration());
        }
        fillimage.fillAmount = FillAmount;
        ready = true;
    }



    IEnumerator rocketgeneration()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        needlaunch = true;

        yield return new WaitUntil(() => canlaunch == true);

        canlaunch = false;
        needlaunch = false;

        GameObject targeti = null;

        FillAmount = 0;

        Vector3 currentPos = player.transform.position;

        float minDist = Mathf.Infinity;

        foreach (GameObject t in Enemy){


            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                minDist = dist;
                targeti = t;
            }
            
        }
        currentPos.y += 2;
        
        if (targeti != null)
        {
            rocket.GetComponent<Rocket>().Target = targeti;
            Instantiate (rocket, currentPos, Quaternion.identity);
        }
    }
}
