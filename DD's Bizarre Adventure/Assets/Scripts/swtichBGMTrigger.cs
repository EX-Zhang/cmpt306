using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class swtichBGMTrigger : MonoBehaviour
{
    public AudioClip newTrack;
    private AudioManager AM;
    private GameObject targetPlayer;
    public bool bossactivate;
    public GameObject boss;
    private float wait = 0;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        AM = FindObjectOfType<AudioManager>();
        bossactivate = false;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (newTrack != null)
            {
                AM.changeBGM(newTrack);
            }
            bossactivate = true;
        }
    }

    void Update()
    {
        if (boss == null)
        {
            text.SetActive(true);
            wait += Time.deltaTime;
            if (wait > 5) {
                SceneManager.LoadScene("Credit");
            }
        }
    }
}
