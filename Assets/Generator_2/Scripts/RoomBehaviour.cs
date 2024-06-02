using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{

    [Header("Wall And Door references")]
    [Tooltip("index 0 = Up wall, index 1 = Down wall, index 2 = Right wall, index 3 = Left wall")]
    [SerializeField] public GameObject[] walls;
    [Tooltip("index 0 = Up door, index 1 = Down door, index 2 = Right door, index 3 = Left door")]
    [SerializeField] public GameObject[] doors;


    /// <summary>
    /// Updates the status of the rooms by setting the walls and doors
    /// to their active or inactive states based on the provided status array.
    /// </summary>
    /// <param name="status"> 
    ///  A boolean array where each element represents the state of a room:
    /// `true` to set the door as active and the wall as inactive, and 
    /// `false` to set the door as inactive and the wall as active.
    /// </param>                                                        
    public void UpdateRooms(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }

    }
}
