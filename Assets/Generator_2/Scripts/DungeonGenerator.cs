using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool isVistied = false;
        public bool[] status = new bool[4];
    }
    [Header("Maze size : ")]
    [SerializeField] private Vector2 size;
    [SerializeField] private int startPosition = 0;

    [SerializeField] List<Cell> board = new List<Cell>();

    [Header("Room Prefab")]
    [SerializeField] private GameObject room;
    [Header("Rooms Offset")]
    [SerializeField] private Vector2 offSet;
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
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

            if (currentCell == board.Count - 1)
            {
                break;
            }
            //Check the cell's neighbors 
            List<int> neighbors = CheckNeighbor(currentCell);
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

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    // newCell going left or right side
                    if (newCell - 1 == currentCell)
                    {
                        // current cell going to right side
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        // next cell opening to left
                        board[currentCell].status[3] = true;
                    }
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

    private List<int> CheckNeighbor(int cell)
    {
        List<int> neighbors = new();

        //Check up neighbor
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].isVistied)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        //Check down neighbor
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].isVistied)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }
        //Check right neighbor
        // Cell % size.x = size.x - 1 
        if ((cell + 1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].isVistied)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //Check left neighbor
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].isVistied)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }
}
