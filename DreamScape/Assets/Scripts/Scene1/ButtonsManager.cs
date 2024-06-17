using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    public void LoadFirstScene()
    {
        // Make sure to replace "NextSceneName" with the actual name of your next scene
        SceneManager.LoadScene("Scene2");
    }

    // This method will be called when the "Quit" button is pressed
    public void QuitGame()
    {
        // Quits the application
        Application.Quit();

        // If the game is running in the Unity editor, this will stop the play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
