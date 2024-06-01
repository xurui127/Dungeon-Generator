using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GungeonGenerator_One : MonoBehaviour
{
    private enum Dirctions { North, South, West, East };


    [Header("Room Info")]
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private int roomCount;
    [SerializeField] private Color startRoomColor;
    [SerializeField] private Color endRoomColor;

    [Header("Offset Setting")]
    [SerializeField] private Transform GenerateCoordinate;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    [Header("Room List")]
    [SerializeField] private List<GameObject> rooms = new();

    private void Start()
    {
        RoomGenerator();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void RoomGenerator()
    {
        for (int i = 0; i < roomCount; i++)
        {
            rooms.Add(Instantiate(roomPrefab, GenerateCoordinate.position, Quaternion.identity));
            RandomGeneratePosition();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startRoomColor;
        // TODO: Find The End Of The Room 
        GetLastRoom();

    }
    // Set Ramdom dirction to room position
    private void RandomGeneratePosition()
    {

        while (IsRoomOverlap(GenerateCoordinate.position))
        {
            var dirction = (Dirctions)Random.Range(0, 4);

            // Random number == 0 , represent the North
            if (dirction == Dirctions.North)
            {
                GenerateCoordinate.position += new Vector3(0, yOffset, 0);
            }
            // Random number == 1 , represent the South
            else if (dirction == Dirctions.South)
            {
                GenerateCoordinate.position += new Vector3(0, -yOffset, 0);
            }
            // Random number == 2 , represent the West
            else if (dirction == Dirctions.West)
            {
                GenerateCoordinate.position += new Vector3(-xOffset, 0, 0);
            }
            // Random number == 3 , represent the East
            else if (dirction == Dirctions.East)
            {
                GenerateCoordinate.position += new Vector3(xOffset, 0, 0);
            }
        }
    }
    // Check new room is overlap with other rooms 
    private bool IsRoomOverlap(Vector3 checkPosition)
    {

        foreach (var room in rooms)
        {
            if (room.transform.position == checkPosition)
            {
                return true;
            }
        }
        return false;
    }
    private void GetLastRoom()
    {
        Queue<GameObject> checkingRoom = new();
        HashSet<GameObject> isVisited = new();

        checkingRoom.Enqueue(rooms[0]);
        isVisited.Add(rooms[0]);
        int step = 0;
        while (checkingRoom.Count != 0)
        {
            int size = checkingRoom.Count;
            for (int i = 0; i < size; i++)
            {
                GameObject currentRoom = checkingRoom.Dequeue();
                

            }
        }

        step++;

    }
}
