using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject enemy;
    Renderer render;
    private bool needrespawn;

    public GameObject respawsenemy;

    private float timer;

    public float waittime = 20.0f;

    private GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        needrespawn = false;
        timer = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null && a == null && !render.isVisible)
        {
            needrespawn = true;
        }

        if (needrespawn) {
            timer += Time.deltaTime;
        }

        if (enemy == null && a == null && !render.isVisible && timer > waittime) {
            needrespawn = false;
            a = Instantiate(respawsenemy, transform.position, Quaternion.identity) as GameObject;
            timer = 0.0f;
        }
    }
}
