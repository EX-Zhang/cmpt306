using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnStart : MonoBehaviour
{
    public void ReturnToStart()
    {
        SceneManager.LoadScene("Menu");
    }
}
