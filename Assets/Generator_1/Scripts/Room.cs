using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [Header("Door Reference")]
    [SerializeField] public GameObject doorSouth;
    [SerializeField] public GameObject doorNorth;
    [SerializeField] public GameObject doorEast;
    [SerializeField] public GameObject doorWest;


    public bool roomSouth;
    public bool roomNorth;
    public bool roomEast;
    public bool roomWest;

    
    
    [SerializeField] private Text roomNumText;
    public int step;
    public int doorNums; 
    
    private void Start()
    {
        doorSouth.SetActive(roomSouth);
        doorNorth.SetActive(roomNorth);
        doorEast.SetActive(roomEast);
        doorWest.SetActive(roomWest);
    }

    public void SetStepToRoom(float xOffset, float yOffset)
    {
        step = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));
        roomNumText.text = step.ToString();
        CheckDoorNum();
    }

    private void CheckDoorNum()
    {
        doorNums += roomSouth ? 1 : 0;
        doorNums += roomNorth ? 1 : 0;
        doorNums += roomEast ? 1 : 0;
        doorNums += roomWest ? 1 : 0;

    
    }
}
