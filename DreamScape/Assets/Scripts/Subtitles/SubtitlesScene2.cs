using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesScene2 : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public GameObject SubtitleBG;
    public bool isDone;
    public bool levelcompleted;

    private void Start()
    {
        //Clear the subtitle text at the start
        subtitleText.text = "";
        SubtitleBG.SetActive(true);
        subtitleText.SetText("Ah...What are all this messages?");
    }

    public void CheckMessages()
    {
        subtitleText.text = ("");
        SubtitleBG.SetActive(false);
    }

    public void AfterCheckMessage()
    {
        SubtitleBG.SetActive(true);
        StartCoroutine(DisplaySubtitles());
    }

    public void CheckBankAccount()
    {
        SubtitleBG.SetActive(true);
        StartCoroutine(FinalSubtitles());
    }

    //Courotine Subtitles
    private IEnumerator DisplaySubtitles()
    {
        subtitleText.SetText("Gas bill? Water bill? Light bill?"); // Display text for 3 seconds
        yield return new WaitForSeconds(4);

        subtitleText.SetText("Ah...there are still this many bills to pay?"); // Display text for 5 seconds
        yield return new WaitForSeconds(6);

        subtitleText.SetText("I dont know if i have enough money...let me check my bank Account."); // Display text for 5 seconds
        yield return new WaitForSeconds(6);

        subtitleText.SetText(""); // Clear the subtitle text
        SubtitleBG.SetActive(false);
        isDone = true;
    }

    private IEnumerator FinalSubtitles()
    {
        subtitleText.SetText("How do I only have this much money left in my account? This isn't possible...");
        yield return new WaitForSeconds(4);
        subtitleText.SetText("Where did I spend all my money?");
        yield return new WaitForSeconds(4);
        subtitleText.SetText("Let me go home and look around the room and see if i can find something...");
        yield return new WaitForSeconds(4);
        subtitleText.SetText("");
        SubtitleBG.SetActive(false);
        levelcompleted = true;
    }
}
