using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameManager1 gameManager;

    public List<string> essentialList = new List<string>();
    public List<string> nonEssentialList = new List<string>();
    public GameObject BNon_Essential;
    public GameObject BEssential;

    private int essentialClickCount = 0;
    private int nonEssentialClickCount = 0;

    public void Essential()
    {
       
        essentialList.Add("shoes");
        Debug.Log("Add to essential List");
        if (gameManager != null)
        {
            gameManager.StartCoroutine(gameManager.WaitShuffle(0.5f));
            Debug.Log("The shuffle was done!");
            BEssential.SetActive(false);
            BNon_Essential.SetActive(false);
            essentialClickCount++;

            if (essentialClickCount >= 3)
            {
                SceneManager.LoadScene("Scene5");
            }
        }
        else
        {
            Debug.LogError("GameManager reference not set in ButtonScript!");
        }
    }

    public void Non_essential()
    {
        nonEssentialList.Add("shoes");
        Debug.Log("Add to Non-essential List");

        if (gameManager != null)
        {
            gameManager.StartCoroutine(gameManager.WaitShuffle(0.5f));
            Debug.Log("The shuffle was done!");
            BEssential.SetActive(false);
            BNon_Essential.SetActive(false);
            nonEssentialClickCount++;

            if (nonEssentialClickCount >= 3)
            {
                SceneManager.LoadScene("Scene5");
            }
        }
        else
        {
            Debug.LogError("GameManager reference not set in ButtonScript!");
        }
    }
}
