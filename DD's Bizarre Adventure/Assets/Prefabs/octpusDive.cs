using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class octpusDive : MonoBehaviour
{
    public float livingtime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livingtime += Time.deltaTime;
        if (livingtime > 20) {
            Destroy(gameObject);
        }
    }
}
