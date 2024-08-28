using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class Scene4Events : MonoBehaviour
{
    //DialogueRunner for the yarn subtitles
    public DialogueRunner dialogueRunner;
   
    
    //--------------------------------Transition Scene 5--------------------------------
    [YarnCommand("changeToScene5")]
    public void TransitionToScene5()
    {
        SceneManager.LoadScene("Scene5");
    }
}
