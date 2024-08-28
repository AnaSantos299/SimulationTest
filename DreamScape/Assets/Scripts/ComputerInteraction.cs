using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ComputerInteraction : MonoBehaviour
{
    private bool isNearPlayer = false;

    //instructions text
    public TextMeshProUGUI InstructionText;
    public GameObject Instructions;

    //SceneName
    public string SceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // when interacting if the tag is player
        {
            InstructionText.text = "Click \"E\" to interact";
            Instructions.SetActive(true);
            Debug.Log("Player Near Computer");
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InstructionText.text = "";
            Instructions.SetActive(false);
            Debug.Log("Player not near Computer");
            isNearPlayer = false;
        }
    }

    private void Update()
    {
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
                InstructionText.text = "";
                Instructions.SetActive(false);
                SceneManager.LoadScene(SceneName);
        }
    }
}
