using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Scene2Events : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 public DialogueRunner dialogueRunner;


    public void SayHello(){
                dialogueRunner.Stop();
        //jumpt to node acceptCall
        dialogueRunner.StartDialogue("SayHelloNode");
    }

    public GameObject phone;

[YarnCommand("makePhoneVisible")]
    public void MakePhoneVisible()
    {

        phone.SetActive(true);
    }

[YarnCommand("makePhoneInvisible")]
    public void MakePhoneInvisible()
    {
        phone.SetActive(false);
    }
}
