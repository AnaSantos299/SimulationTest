using UnityEngine;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{
    [SerializeField] private Transform[] currentPuzzlePieces; // Current puzzle pieces
    [SerializeField] private GameObject winText; //WinText
    public GameObject essentialButton; //Essential Button
    public GameObject nonEssentialButton; //Non-Essential Button
    public GameObject explainText; //Text explaining the Buttons
    public static bool youWin; //Player completed the puzzle

    private List<string> essentialPuzzles = new List<string>(); //List of puzzles considered essential
    private List<string> nonEssentialPuzzles = new List<string>(); //List of puzzles considered non-essential

    public List<PuzzleData> puzzles = new List<PuzzleData>(); //List with the puzzles information

    private int currentPuzzleIndex = 0; //Current puzzle

    private void Start()
    {
        SetUIState(false); //UI Initialization
        youWin = false;
        LoadPuzzle(currentPuzzleIndex); //Load the current puzzle
    }

    private void Update()
    {
        //If the puzzle isnt completed and all the pieces are in the correct place
        if (!youWin && AllPiecesInCorrectPosition())
        {
            //Show UI elements when the puzzle is completed
            SetUIState(true);
            Debug.Log("Puzzle Completed: " + puzzles[currentPuzzleIndex].name);
        }
    }

    private bool AllPiecesInCorrectPosition()
    {
        //if the pieces are null
        if (currentPuzzlePieces == null)
            return false;
        //check if all the pieces are correctly rotated
        foreach (Transform piece in currentPuzzlePieces)
        {
            //if the rotation on the z axis is different then 0 return false
            if (piece.rotation.z != 0)
            {
                return false;
            }
        }
        return true;
    }

    public void OnEssentialButtonClick()
    {
        //Hide current puzzle and add to essential list
        HideCurrentPuzzle();
        essentialPuzzles.Add(puzzles[currentPuzzleIndex].name);
        SwitchToNextPuzzle();
    }

    public void OnNonEssentialButtonClick()
    {
        //Hide the current puzzle and add to non-essential list
        HideCurrentPuzzle();
        nonEssentialPuzzles.Add(puzzles[currentPuzzleIndex].name);
        SwitchToNextPuzzle();
    }
    
    private void LoadPuzzle(int index)
    {
        if (index >= 0 && index < puzzles.Count)
        {
            //Hide current puzzle, reset flags, and load new puzzle
            HideCurrentPuzzle();
            youWin = false;
            SetUIState(false);

            currentPuzzlePieces = puzzles[index].pieces;
            ShowCurrentPuzzle();
        }
    }

    private void HideCurrentPuzzle()
    {
        //Hide all puzzle pieces
        if (currentPuzzlePieces != null)
        {
            foreach (Transform piece in currentPuzzlePieces)
            {
                piece.gameObject.SetActive(false);
            }
        }
    }

    private void ShowCurrentPuzzle()
    {
        //Show all puzzle pieces
        if (currentPuzzlePieces != null)
        {
            foreach (Transform piece in currentPuzzlePieces)
            {
                piece.gameObject.SetActive(true);
            }
        }
    }

    private void SwitchToNextPuzzle()
    {
        //Move to the next puzzle
        currentPuzzleIndex++;
        if (currentPuzzleIndex >= puzzles.Count)
        {
            Debug.Log("No more puzzles!");
        }
        else
        {
            //Load next puzzle
            LoadPuzzle(currentPuzzleIndex);
        }
    }

    private void SetUIState(bool state)
    {
        //Set visibility of UI elements
        winText.SetActive(state);
        essentialButton.SetActive(state);
        nonEssentialButton.SetActive(state);
        explainText.SetActive(state);
    }
}

// PuzzleData.cs
[System.Serializable]
public class PuzzleData
{
    //Name of puzzle
    public string name;
    //Pieces of the puzzle
    public Transform[] pieces;
    //Constructor
    public PuzzleData(string puzzleName, Transform[] puzzlePieces)
    {
        name = puzzleName;
        pieces = puzzlePieces;
    }
}