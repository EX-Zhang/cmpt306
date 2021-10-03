using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameButton : MonoBehaviour
{
    public void restartScene()
    {
        SceneManager.LoadScene("Main");
    }
}
