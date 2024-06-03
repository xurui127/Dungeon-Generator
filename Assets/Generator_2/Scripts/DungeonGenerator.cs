/// Credit to SilverlyBee for the original implementation.
//  Video:  https://www.youtube.com/watch?v=gHU5RQWbmWE
//  GitHub: https://github.com/silverlybee/dungeon-generator  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
     
    /// <summary>
    /// record cell properties
    /// isVistied to keep room does not regenerate 
    /// status for save 4 dirctions rooms 0 = Up, 1 = Down, 2 = Right, 3 = Left
    /// </summary>
    public class Cell
    {
        public bool isVistied = false;
        public bool[] status = new bool[4];
    }
    [Header("Maze size : ")]
    // for maze size.
    // size.x for the row
    // size.y for the column
    [SerializeField] private Vector2 size;
    
    // Set room start spawn position
    [SerializeField] private int startPosition = 0;

    [Header("Room Prefab")]
    [SerializeField] private GameObject room;
    
    [Header("Rooms Offset")]
    [SerializeField] private Vector2 offSet;
    
    // for save room status 
    [SerializeField] List<Cell> board = new List<Cell>();
   

    void Start()
    {
        MazeGenerator();
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            ReloadScene();
        }
    }

    private void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if (currentCell.isVistied)
                {
                    var newRoom = Instantiate(room, new Vector2(i * offSet.x, -j * offSet.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRooms(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }
               
            }
        }
    }

    // Generate Maze 
    private void MazeGenerator()
    {

        board = new();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPosition;

        Stack<int> path = new();

        int k = 0;
        while (k < 1000)
        {
            k++;

            board[currentCell].isVistied = true;

            // Check if currentCell is the last Cell
            if (currentCell == board.Count - 1)
            {
                break;
            }
            
            //Check the cell's neighbors 
            List<int> neighbors = CheckNeighbors(currentCell);


            // Back Trace to the start position
            if (neighbors.Count == 0)
            {
                // to the end of the path
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                // Randomly pick a neighbor
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // newCell going left or right side
                if (newCell > currentCell)
                {
                    // if newCell = 2 and currentCell = 1,
                    // the newCell should be at the currentCell right side 
                    if (newCell - 1 == currentCell)
                    {
                        // current cell going to right side
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        // next cell opening to left
                        board[currentCell].status[3] = true;
                    }
                    // if(newCell - 1 != currenCell)
                    // Ex. newCell = 12 and currentCell = 2
                    // means newCell is at the currentCell down side
                    else
                    {
                        // current cell going to down side
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        //next cell going to up side
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    // newCell going Up or Left side
                    if (newCell + 1 == currentCell)
                    {
                        // currentCell going to left side
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        // next cell opening to right
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        // current cell going to down side
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        // next cell opening on the up side
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    private List<int> CheckNeighbors(int currentCell)
    {
        List<int> neighbors = new();

        //Check up neighbor
        if (currentCell - size.x >= 0 && !board[Mathf.FloorToInt(currentCell - size.x)].isVistied)
        {
            neighbors.Add(Mathf.FloorToInt(currentCell - size.x));
        }
        //Check down neighbor
        if (currentCell + size.x < board.Count && !board[Mathf.FloorToInt(currentCell + size.x)].isVistied)
        {
            neighbors.Add(Mathf.FloorToInt(currentCell + size.x));
        }
        //Check right neighbor
        //      (1 + 1) % 10 = 2                                         2
        //      (2 + 1) % 10 = 3
        //      (9 + 1) % 10 = 0   means last index in first row         9 + 1 = 10
        //      (19 + 1)% 10 = 0                                        19 + 1 = 20
        if ((currentCell + 1) % size.x != 0 && !board[Mathf.FloorToInt(currentCell + 1)].isVistied)
        {
            neighbors.Add(Mathf.FloorToInt(currentCell + 1));
        }

        //Check left neighbor
        if (currentCell % size.x != 0 && !board[Mathf.FloorToInt(currentCell - 1)].isVistied)
        {
            neighbors.Add(Mathf.FloorToInt(currentCell - 1));
        }

        return neighbors;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
