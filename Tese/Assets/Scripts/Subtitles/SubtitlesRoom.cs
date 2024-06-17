using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitlesRoom : MonoBehaviour
{
    
    public TextMeshProUGUI subtitleText;
    public GameObject SubtitleBG;

    private void Start()
    {
        //Clear the subtitle text at the start
        subtitleText.text = "";
        SubtitleBG.SetActive(true);
        //Start the subtitles
        StartCoroutine(DisplaySubtitles());
    }

    // Example Coroutine to display subtitles
    private IEnumerator DisplaySubtitles()
    {
        subtitleText.SetText("Where did I spend all my money?"); // Display text for 3 seconds
        yield return new WaitForSeconds(4);

        subtitleText.SetText("Let me look around the room and see if I can find out something..."); // Display text for 5 seconds
        yield return new WaitForSeconds(6);

        subtitleText.SetText(""); // Clear the subtitle text
        SubtitleBG.SetActive(false);
    }
}
