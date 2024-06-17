using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipGame : MonoBehaviour
{
    public void SkipGameButton()
    {
        SceneManager.LoadScene("Scene5");
    }
}
