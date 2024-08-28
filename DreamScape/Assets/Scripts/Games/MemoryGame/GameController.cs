using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<Button> btns = new List<Button>();

    [SerializeField] private Sprite bgImage;

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorretGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Images/PuzzleImages");   
    }

    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        gameGuesses = gamePuzzles.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for(int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }
    void AddGamePuzzles()
    {
        int index = 0;

        for (int i = 0; i < btns.Count; i++)
        {
            if(index == btns.Count / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);

            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }
    public void PickAPuzzle()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            countGuesses++;

            StartCoroutine(CheckIfPuzzlesMatch());
        }
    }

    IEnumerator CheckIfPuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);

        if ((firstGuessPuzzle == secondGuessPuzzle && firstGuessIndex != secondGuessIndex))
        {
            yield return new WaitForSeconds(0.5f);

            //Make the buttons NOT interactable
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            //Make the buttons invisible
            btns[firstGuessIndex].image.color = new Color (0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            Debug.Log("The puzzle matches");
            CheckIfTheGameIsFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(0.5f);

        firstGuess = false;
        secondGuess = false;
    }

    void CheckIfTheGameIsFinished()
    {
        countCorretGuesses++;

        if (countCorretGuesses == gameGuesses)
        {
            Debug.Log("Game Completed");
            Debug.Log("It took you" + countGuesses + "Guesses");
            SceneManager.LoadScene("Scene11");
        }
    }

    void ShufflePuzzles(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            //temporary variable
            Sprite temp = list[i];
            //returns a number between 0 and the number of the list count - 1
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
