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
    [SerializeField] private List<Room> rooms = new();


    
    
    //furthest and second farthest rooms perameters 
    // save furthest rooms
    private List<GameObject> furthestRooms = new();
    // save second farthest rooms
    private List<GameObject> lessFurtherRooms = new();
    // save both of rooms only with one door
    private List<GameObject> singleDoorRooms = new();

    private int maxstep = 0;

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
            rooms.Add(Instantiate(roomPrefab, GenerateCoordinate.position, Quaternion.identity).GetComponent<Room>());
            RandomGeneratePosition();
        }
        SetUpRooms();
        FindFurthestRoom();
        FindLessFarestRoom();
        rooms[0].GetComponent<SpriteRenderer>().color = startRoomColor;
        GetSingleDoorRoom().GetComponent<SpriteRenderer>().color = endRoomColor;





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
    private GameObject GetLastRoom()
    {
        var lastRoom = rooms[0].gameObject;
        foreach (var room in rooms)
        {

            if (room.transform.position.sqrMagnitude > lastRoom.transform.position.magnitude)
            {
                lastRoom = room.gameObject;
            }
        }
        return lastRoom;
    }
    private void SetUpRooms()
    {
        foreach (var room in rooms)
        {
            var roomPosition = room.transform.position;
            room.roomNorth = IsRoomOverlap(roomPosition + new Vector3(0, -yOffset, 0));
            room.roomSouth = IsRoomOverlap(roomPosition + new Vector3(0, yOffset, 0));
            room.roomEast = IsRoomOverlap(roomPosition + new Vector3(xOffset, 0, 0));
            room.roomWest = IsRoomOverlap(roomPosition + new Vector3(-xOffset, 0, 0));
            
            //Setup the room step text
            room.SetStepToRoom();
        }

    }

    private void SetBossRoom()
    {
       
        GetSingleDoorRoom();
    }

    private void FindFurthestRoom()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].step > maxstep)
            {
                maxstep = rooms[i].step;
            }
        }
    }

    private void FindLessFarestRoom()
    {
        foreach (var room in rooms)
        {
            if (room.step == maxstep)
            {
                furthestRooms.Add(room.gameObject);
            }
            if (room.step == maxstep - 1)
            {
                lessFurtherRooms.Add(room.gameObject);
            }
        }
    }

    private GameObject GetSingleDoorRoom()
    {
        
        for(int i =  0; i < furthestRooms.Count; i++)
        {
            if (furthestRooms[i].GetComponent<Room>().doorNums == 1)
            {
                singleDoorRooms.Add(furthestRooms[i]);
            }
        }
        for (int i = 0; i < lessFurtherRooms.Count; i++)
        {
            if (lessFurtherRooms[i].GetComponent<Room>().doorNums == 1)
            {
                singleDoorRooms.Add(lessFurtherRooms[i]);
            }
        }

        if (singleDoorRooms.Count != 0)
        {
            return singleDoorRooms[Random.Range(0, singleDoorRooms.Count)];
        }
        else
        {
            return furthestRooms[Random.Range(0, furthestRooms.Count)];
        }
        

    }

}
