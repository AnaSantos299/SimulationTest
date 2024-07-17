using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLogic : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;

    public GameObject EssentialBT;
    public GameObject Non_EssentialBT;

    public GameObject BG;

    // Start is called before the first frame update
    void Start()
    {
        //initialize the list
        pieces = new List<Transform>();
        //the puzzle will be 3 by 3
        size = 3;
        CreateGamePieces(0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Go through the list, the index tells us the position
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        //check each direction to see if valid move
                        //We break out on success so we don't carry on a swap back again
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) { break; }
                    }
                }
            }
        }
    }

    private void CreateGamePieces(float gapThickness)
    {
        //this is the width of each tile
        float width = 1 / (float)size;
        //iterate over the size x size 2D array
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                //Pieces will be in a game board going from -1 to +1
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                 +1 - (2 * width * row) - width,
                                                 0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";
                // We want an empty space in the bottom right
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    //We want to map the UV coordinates appropriately, they are 0->1
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    //UV coord order: (0, 1), (1,1), (0,0), (1,0)
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                    //assign our new UV's to the mesh.
                    mesh.uv = uv;
                }

                if (!shuffling && CheckCompletion())
                {
                    shuffling = true;
                    StartCoroutine(WaitShuffle(0.5f));
                }
            }
        }
    }

    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            //swap them in game state
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            //swap their transforms
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            //update empty location
            emptyLocation = i;

            // Check for completion after each valid move
            if (!shuffling && CheckCompletion())
            {
                EssentialBT.SetActive(true);
                Non_EssentialBT.SetActive(true);
            } else
            {
                EssentialBT.SetActive(false);
                Non_EssentialBT.SetActive(false);
            }

            return true;
        }
        return false;
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }

    public IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            int rnd = Random.Range(0, size * size);
            if (rnd == last) { continue; }
            last = emptyLocation;

            if (SwapIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +1, size - 1))
            {
                count++;
            }
        }
    }
}