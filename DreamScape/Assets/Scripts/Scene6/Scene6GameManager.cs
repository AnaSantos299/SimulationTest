using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene6GameManager : MonoBehaviour
{
    //variable to know which page is being shown
    private int browserPage;
    //BackArrowButton
    [SerializeField] private GameObject backArrow;
    //The image component to change
    public Image targetImage;
    //the sprite to change to when click forward
    public Sprite fowardSprite;
    //the sprite to change to when click back
    public Sprite backSprite;



    public void FowardArrow()
    {
        Debug.Log("ButtonClicked");
        browserPage++;
        backArrow.SetActive(true);
        targetImage.sprite = fowardSprite;

        if (browserPage >= 2)
        {
            SceneManager.LoadScene("Scene8");
        }
    }

  public void BackArrow()
    {
        Debug.Log("ButtonClicked");
        targetImage.sprite = backSprite;
        browserPage--;
        backArrow.SetActive(false);
    }
}
