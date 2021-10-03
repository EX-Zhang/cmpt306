using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e4controoler : MonoBehaviour
{
    float maxspeed = 5f;
    bool isfacingright = true;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float direction = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(direction * maxspeed));
        if(direction > 0 && isfacingright == false){
            Flip();
        }
        else if (direction < 0 && isfacingright == true){
            Flip();
        }
    }

    void Flip(){
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }
}
