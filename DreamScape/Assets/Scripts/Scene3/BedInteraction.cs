using UnityEngine;
using Yarn.Unity;
using TMPro;

public class BedInteraction : MonoBehaviour
{
    public Scene3Events scene3Events; // Reference to the main script

    private bool isNearPlayer = false;

    //instructions text
    public TextMeshProUGUI InstructionText;
    public GameObject Instructions;

    // Dialogue
    public DialogueRunner dialogueRunner;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && scene3Events.allObjectsCollected == true) // Ensure your player has the "Player" tag
        {
            InstructionText.text = "Click \"E\" to interact";
            Instructions.SetActive(true);
            Debug.Log("Player Near Bed");
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && scene3Events.allObjectsCollected == true)
        {
            InstructionText.text = "";
            Instructions.SetActive(false);
            Debug.Log("Player not near Bed");
            isNearPlayer = false;
        }
    }

    private void Update()
    {
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            // Check if all objects have been collected before starting the coroutine
            if (scene3Events != null && scene3Events.allObjectsCollected == true)
            {
                InstructionText.text = "";
                Instructions.SetActive(false);
                StartCoroutine(scene3Events.DisplayImagesForTransition());
            }
        }
    }
}
