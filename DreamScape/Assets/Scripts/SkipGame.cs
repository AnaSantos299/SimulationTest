using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipGame : MonoBehaviour
{
    public string SceneName;
    public void SkipGameButton()
    {
        SceneManager.LoadScene(SceneName);
    }
}
