using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Door Info")]
    [SerializeField] public GameObject doorSouth;
    [SerializeField] public GameObject doorNorth;
    [SerializeField] public GameObject doorEast;
    [SerializeField] public GameObject doorWest;


    public bool roomSouth;
    public bool roomNorth;
    public bool roomEast;
    public bool roomWest;


    private void Start()
    {
        doorSouth.SetActive(roomSouth);
        doorNorth.SetActive(roomNorth);
        doorEast.SetActive(roomEast);
        doorWest.SetActive(roomWest);
    }
}
